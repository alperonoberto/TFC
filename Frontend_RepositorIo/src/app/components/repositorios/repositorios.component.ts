import { Component } from '@angular/core';
import { LISTA_REPOSITORIOS, LISTA_ARCHIVOS_1, LISTA_ARCHIVOS_2 } from './constants/index';

@Component({
  selector: 'app-repositorios',
  templateUrl: './repositorios.component.html',
  styleUrls: ['./repositorios.component.scss']
})
export class RepositoriosComponent {

  public repositorioActual: number = 0;
  public repositorioActualNombre: string = '';

  public listaRepositorios = [...LISTA_REPOSITORIOS];
  public listaArchivos = [...LISTA_ARCHIVOS_1];

  mostrarArchivos(repositorio: any) {
    this.repositorioActual = repositorio.id;
    this.repositorioActualNombre = repositorio.nombre;
    if(repositorio.id === 2) {
      this.listaArchivos = [...LISTA_ARCHIVOS_2]
    } else {
      this.listaArchivos = [...LISTA_ARCHIVOS_1]
    }
  }

}
