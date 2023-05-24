import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BASE_URL } from 'src/app/environment/environment.constants';

@Injectable({
  providedIn: 'root'
})
export class RepositorioService {

  private urlGetRepositorioByUser = BASE_URL + 'repositorios/user/';
  private urlGetRepositorios = BASE_URL + 'repositorios';
  private urlGetRepositorioById = BASE_URL + 'repositorios/';
  private urlPostRepositorio = BASE_URL + 'repositorios/add';
  private urlPutRepositorio = BASE_URL + 'repositorios/update';
  private urlDeleteRepositorioById = BASE_URL + 'repositorios/delete/';

  constructor(
    protected _http: HttpClient
  ) { }

  public getRepositorioByUser(userId: number) {
    return this._http.get(this.urlGetRepositorioByUser + userId);
  } 
}
