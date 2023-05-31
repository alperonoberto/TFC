import { Component, OnInit } from '@angular/core';
import {
  LISTA_REPOSITORIOS,
  LISTA_ARCHIVOS_1,
  LISTA_ARCHIVOS_2,
} from './constants/index';
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
  selectedFiles: any[] = [];

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
    this._loginService.getUserLoggedIn().subscribe((res) => {
      this._repoService.getRepositorioByUser(res['id']).subscribe((res) => {
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
      console.log(this.listaArchivos);
    });
  }

  public onSubmit() {

    this.isCreandoRepo = false;
  }

  onFileSelected(event: any): void {
    this.selectedFiles = [...event.target.files]
    console.table(this.selectedFiles)
  }


}
