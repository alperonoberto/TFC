import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BASE_URL } from 'src/app/environment/environment.constants';

@Injectable({
  providedIn: 'root'
})
export class RepositorioService {

  // EJEMPLO POSTEO DE REPOSITORIO
  // {
  //   "nombre": "repositorio cool",
  //   "descripcion": "prueba de crear carpetaas",
  //   "fechaMod": "2023-05-31T18:48:39.037Z",
  //   "usuarioId": 1
  // }

  private urlGetRepositorioByUser = BASE_URL + 'repositorios/user/';
  private urlGetRepositorios = BASE_URL + 'repositorios';
  private urlGetRepositorioById = BASE_URL + 'repositorios/';
  private urlPostRepositorio = BASE_URL + 'repositorios/add';
  private urlPutRepositorio = BASE_URL + 'repositorios/update';
  private urlDeleteRepositorioById = BASE_URL + 'repositorios/delete/';

  constructor(
    protected _http: HttpClient
  ) { }

  public getRepositoriosByUser(userId: number) {
    return this._http.get(this.urlGetRepositorioByUser + userId);
  }

  public getRepositorioById(repoId: number) {
    return this._http.get(this.urlGetRepositorioById + repoId);
  }

  public postRepositorio(repositorio: any) {
    return this._http.post(this.urlPostRepositorio, repositorio);
  }

  public deleteRepositorio(id) {
    return this._http.delete(this.urlDeleteRepositorioById + id);
  }
}
