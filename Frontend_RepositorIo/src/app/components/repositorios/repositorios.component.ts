import {
  AfterContentInit,
  Component,
  Directive,
  EventEmitter,
  OnInit,
  Output,
} from '@angular/core';
import { RepositorioService } from '../services/repositorio/repositorio.service';
import { LoginService } from '../services/login/login.service';
import { ArchivoService } from '../services/archivo/archivo.service';
import { FormControl, FormGroup } from '@angular/forms';
import { WarningModalComponent } from 'src/app/shared/components/warningModal/warning-modal.component';
import { MatDialog } from '@angular/material/dialog';
import { saveAs } from 'file-saver';

@Component({
  selector: 'app-repositorios',
  templateUrl: './repositorios.component.html',
  styleUrls: ['./repositorios.component.scss'],
})
export class RepositoriosComponent implements OnInit {
  public repositorioActual: number = 0;
  public repositorioActualNombre: string = '';
  public isCreandoRepo: boolean;
  public selectedFiles: any[] = [];
  public userLoggedIn: any;

  public listaRepositorios = [];
  public listaArchivos = [];
  public filesToDelete = [];
  public filesToDownloadIds = [];

  private repositorio: any;

  repositoriosForm = new FormGroup({
    titulo: new FormControl(''),
    descripcion: new FormControl(''),
    archivos: new FormControl(),
  });

  constructor(
    private _repoService: RepositorioService,
    private _loginService: LoginService,
    private _archivoService: ArchivoService,
    private dialog: MatDialog
  ) {}

  public ngOnInit(): void {
    this._loginService.getUserLoggedIn().subscribe((res) => {
      this.userLoggedIn = res;
      this.mostrarRepositorios(this.userLoggedIn);
    });
  }

  mostrarArchivos(repositorio: any) {
    this.repositorio = repositorio;
    this.isCreandoRepo = false;
    this.repositorioActual = repositorio.id;
    this.repositorioActualNombre = repositorio.nombre;

    this._archivoService.getArchivosByRepo(repositorio.id).subscribe((res) => {
      this.listaArchivos = [...[res]].flat();
      console.table(this.listaArchivos);
    });
  }

  mostrarRepositorios(user: any) {
    this._repoService.getRepositoriosByUser(user['id']).subscribe((res) => {
      this.listaRepositorios = [...[res]].flat();
    });
  }

  public onSubmit(event: any) {
    let repositorio = {
      nombre: this.repositoriosForm.get('titulo').value.replaceAll(' ', '_'),
      descripcion: this.repositoriosForm.get('descripcion').value,
      usuarioId: this.userLoggedIn.id,
    };

    try {
      this._repoService.postRepositorio(repositorio).subscribe(
        (res) => {
          console.log(res);
        },
        (err) => {
          console.log(err);
        }
      );
    } finally {
      if (this.repositoriosForm.get('archivos').value != null) {
        let form = new FormData();
        this.selectedFiles.forEach((file) => {
          form.append('file', file);
        });

        this._archivoService
          .postArchivos(form, repositorio.usuarioId, repositorio.nombre)
          .subscribe(
            (res) => {
              console.log(res);
            },
            (err) => {
              console.log(err);
            }
          );
      }
    }

    this.isCreandoRepo = false;
  }

  onFileSelected(event: any): void {
    this.selectedFiles = [...event.target.files];
    console.log(this.selectedFiles);
  }

  uploadFiles(event: any): void {
    this.selectedFiles = [...event.target.files];
    let form = new FormData();
    this.selectedFiles.forEach((file) => {
      form.append('file', file);
    });

    this._repoService.getRepositorioById(this.repositorioActual).subscribe(
      (res) => {
        let repositorio = res;

        this._archivoService
          .postArchivos(form, repositorio['usuarioId'], repositorio['nombre'])
          .subscribe(
            (res) => {
              console.log(res);
              this.isCreandoRepo ? (this.selectedFiles = []) : null;
            },
            (err) => {
              console.error(err);
            }
          );
      },
      (err) => {
        console.error(err);
      }
    );
  }

  downloadFiles() {
    const files = this.getSelectedFiles();
    this.filesToDownloadIds = [];
    files.forEach((file) => {
      this.filesToDownloadIds.push(file['id']);
    });

    if (files.length == 0) {
      const dialogRef = this.dialog.open(WarningModalComponent, {
        maxWidth: '800px',
        data: {
          title: `Selecciona algún archivo`,
          message: '',
          isGeneralPurposeModal: true,
        },
      });
    } else if (files.length == 1) {
      this._archivoService.downloadFile(files[0]['id']).subscribe(
        (res) => {
          console.log(res);
          saveAs(res, files[0]['filename']);
        },
        (err) => {
          console.log(err);
        }
      );
    } else {
      this._archivoService.downloadFiles(this.filesToDownloadIds).subscribe(
        (res) => {
          console.log(res);
          const blob = new Blob([res], { type: 'application/zip' });
          saveAs(blob, this.repositorioActualNombre);
        },
        (err) => {
          console.log(err);
        }
      );
    }
  }

  onDelete(repo) {
    const dialogRef = this.dialog.open(WarningModalComponent, {
      maxWidth: '800px',
      data: {
        title: ` repositorio ${repo.nombre}`,
        message: 'Confirma tu elección:',
      },
    });

    dialogRef.afterClosed().subscribe((res) => {
      if (res) {
        this._repoService.deleteRepositorio(repo.id).subscribe(
          (res) => {
            this.mostrarRepositorios(this.userLoggedIn);
            console.log(res);
          },
          (err) => {
            console.log(err);
          }
        );
      }
    });
  }

  onDeleteFiles() {
    const files = this.getSelectedFiles();
    const filesToDeleteNames = [];
    files.forEach((file) => {
      filesToDeleteNames.push(file['filename']);
    });

    const dialogRef = this.dialog.open(WarningModalComponent, {
      maxWidth: '800px',
      data: {
        title: ` archivos`,
        message: 'Confirma tu elección:',
        files: filesToDeleteNames,
      },
    });

    dialogRef.afterClosed().subscribe((res) => {
      if (res) {
        this.filesToDelete.forEach((file) => {
          this._archivoService.deleteArchivo(file).subscribe(
            (res) => {
              console.log(res);
              this.mostrarArchivos(this.repositorio);
            },
            (err) => {
              console.error(err);
            }
          );
        });
      }
    });
  }

  getSelectedFiles(): string[] {
    return this.filesToDownloadIds;
  }

  addSelectedFile(event: any, file) {
    if (event.checked) {
      this.filesToDownloadIds.push(file);
    } else if (this.filesToDownloadIds.includes(file)) {
      let index = this.filesToDownloadIds.findIndex((f) => f.id == file.id);
      this.filesToDownloadIds.splice(index, 1);
    }
  }

  vaciarListas() {
    this.selectedFiles = [];
    this.filesToDelete = []; 
    this.filesToDownloadIds = [];
  }
}

@Directive({ selector: '[after-if]' })
export class AfterIfDirective implements AfterContentInit {
  @Output('after-if')
  public after: EventEmitter<void> = new EventEmitter<void>();

  public ngAfterContentInit(): void {
    // timeout helps prevent unexpected change errors
    setTimeout(() => this.after.next());
  }
}
