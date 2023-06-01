import { Component, OnInit } from '@angular/core';
import { RepositorioService } from '../services/repositorio/repositorio.service';
import { LoginService } from '../services/login/login.service';
import { ArchivoService } from '../services/archivo/archivo.service';
import { FormControl, FormGroup } from '@angular/forms';

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

  repositoriosForm = new FormGroup({
    titulo: new FormControl(''),
    descripcion: new FormControl(''),
  });

  constructor(
    private _repoService: RepositorioService,
    private _loginService: LoginService,
    private _archivoService: ArchivoService
  ) {}

  public ngOnInit(): void {
    console.log(this.selectedFiles)
    console.log(this.listaArchivos)
    this._loginService.getUserLoggedIn().subscribe((res) => {
      this.userLoggedIn = res;
      this._repoService.getRepositoriosByUser(res['id']).subscribe((res) => {
        this.listaRepositorios = [...[res]].flat();
      });
    });
  }

  mostrarArchivos(repositorio: any) {
    this.isCreandoRepo = false;
    this.repositorioActual = repositorio.id;
    this.repositorioActualNombre = repositorio.nombre;

    this._archivoService.getArchivosByRepo(repositorio.id).subscribe((res) => {
      this.listaArchivos = [...[res]].flat();
    });
  }

  public onSubmit() {
    let repositorio = {
      nombre: this.repositoriosForm.get('titulo').value.replaceAll(' ', '_'),
      descripcion: this.repositoriosForm.get('descripcion').value,
      usuarioId: this.userLoggedIn.id,
    };
    console.log(repositorio);
    this._repoService.postRepositorio(repositorio).subscribe((res) => {
      console.log(res);

      let form = new FormData();
      this.listaArchivos.forEach((file) => {
        form.append('file', file);
      });

      this._archivoService
        .postArchivos(form, repositorio.usuarioId, repositorio.nombre)
        .subscribe(
          (res) => {
            console.log(res);
          },
          (err) => {
            console.error(err);
          }
        );
    });

    this.isCreandoRepo = false;
  }

  onFileSelected(event: any): void {
    this.selectedFiles = [...event.target.files];
    console.table(this.selectedFiles);
  }

  uploadFiles(event: any): void {
    this.selectedFiles = [...event.target.files];

    let form = new FormData();
    this.selectedFiles.forEach((file) => {
      form.append('file', file);
    });

    this._repoService.getRepositorioById(this.repositorioActual).subscribe(
      (res) => {
        console.log(res["usuarioId"]+"/"+ res["nombre"]);
        let repositorio = res;

        this._archivoService
          .postArchivos(form, repositorio["usuarioId"], repositorio["nombre"])
          .subscribe(
            (res) => {
              console.log(res);
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

  onDelete(repo) {
    this._repoService.deleteRepositorio(repo.id).subscribe(
      (res) => {
        console.log(res);
      },
      (err) => {
        console.log(err);
      }
    );
  }
}
