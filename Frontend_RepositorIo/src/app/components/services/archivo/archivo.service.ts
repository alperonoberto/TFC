import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from 'src/app/environment/environment.constants';

@Injectable({
  providedIn: 'root'
})
export class ArchivoService {

  private urlGetArchivosByRepo = BASE_URL + 'archivos/repositorio/';
  private urlPostArchivos = BASE_URL + 'archivos/upload/';

  constructor(
    private http: HttpClient
  ) { }

  public getArchivosByRepo(id: number) {
    return this.http.get(this.urlGetArchivosByRepo + id);
  }

  public postArchivos(files: FormData, user, repositorio) {
    return this.http.post(`${this.urlPostArchivos}${user}/${repositorio}`, files)
  }

}
