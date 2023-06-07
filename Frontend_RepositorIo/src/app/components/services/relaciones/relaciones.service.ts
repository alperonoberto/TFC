import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BASE_URL } from 'src/app/environment/environment.constants';

@Injectable({
  providedIn: 'root'
})
export class RelacionesService {

  constructor(
    protected _http: HttpClient
  ) { }

  private urlGetRelaciones = BASE_URL + 'relaciones';
  private urlGetRelacionById = BASE_URL + 'relaciones/';
  private urlGetRelacionesByUser = BASE_URL + 'relaciones/user/';
  private urlPostRelacion = BASE_URL + 'relaciones/add';
  private urlPutRelacion = BASE_URL + 'relaciones/update';
  private urlDeleteRelacion = BASE_URL + 'relaciones/delete';

  public GetRelaciones() {
    return this._http.get(this.urlGetRelaciones);
  }

  public GetRelacionById(relacionId: number) {
    return this._http.get(this.urlGetRelacionById + relacionId);
  }

  public GetRelacionesByUser(userId: number) {
    return this._http.get(this.urlGetRelacionesByUser + userId);
  }

  public PostRelacion(relacion: any) {
    return this._http.post(this.urlPostRelacion, relacion);
  }

  public PutRelacion(relacion: any) {
    return this._http.put(this.urlPutRelacion, relacion);
  }

  public DeleteRelacion(relacionId: number) {
    return this._http.get(this.urlDeleteRelacion);
  }
}
