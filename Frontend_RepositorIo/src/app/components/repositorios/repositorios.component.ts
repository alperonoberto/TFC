import { Component } from '@angular/core';

@Component({
  selector: 'app-repositorios',
  templateUrl: './repositorios.component.html',
  styleUrls: ['./repositorios.component.scss']
})
export class RepositoriosComponent {

  public repositorioActual: number = 0;

  public listaRepositorios = [
    {
      id: 1,
      nombre: "Repositorio1",
      descripcion: "Descripcion repositorio 1",
      fechaMod: null,
      cuentaId: 1
    },
    {
      id: 2,
      nombre: "Repositorio2",
      descripcion: "Descripcion repositorio 2",
      fechaMod: null,
      cuentaId: 1
    },
    {
      id: 3,
      nombre: "Repositorio3",
      descripcion: "Descripcion repositorio 3",
      fechaMod: null,
      cuentaId: 1
    },
    {
      id: 4,
      nombre: "Repositorio4",
      descripcion: "Descripcion repositorio 4",
      fechaMod: null,
      cuentaId: 1
    },
    {
      id: 5,
      nombre: "Repositorio5",
      descripcion: "Descripcion repositorio 5",
      fechaMod: null,
      cuentaId: 1
    }
  ];

  public listaArchivos = [
    {
      nombre: "archivo1.jpg",
      fechaMod: null,
      repositorioId: 1
    },
    {
      nombre: "archivo2.pdf",
      fechaMod: null,
      repositorioId: 1
    },
    {
      nombre: "archivo3.html",
      fechaMod: null,
      repositorioId: 1
    },
    {
      nombre: "archivo4.md",
      fechaMod: null,
      repositorioId: 1
    },
    {
      nombre: "archivo5.scss",
      fechaMod: null,
      repositorioId: 1
    },
  ];

  mostrarArchivos(repositorioId: number) {
    this.repositorioActual = repositorioId;
  }

}
