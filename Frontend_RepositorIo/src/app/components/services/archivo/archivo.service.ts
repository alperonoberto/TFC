import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BASE_URL } from 'src/app/environment/environment.constants';

@Injectable({
  providedIn: 'root',
})
export class ArchivoService {
  private urlGetArchivosByRepo = BASE_URL + 'archivos/repositorio/';
  private urlPostArchivos = BASE_URL + 'archivos/upload/';
  private urlDeleteArchivos = BASE_URL + 'archivos/delete/';
  private urlDownloadFiles = BASE_URL + 'archivos/download';

  constructor(private http: HttpClient) {}

  public getArchivosByRepo(id: number) {
    return this.http.get(this.urlGetArchivosByRepo + id);
  }

  public postArchivos(files: FormData, user, repositorio) {
    return this.http.post(
      `${this.urlPostArchivos}${user}/${repositorio}`,
      files
    );
  }

  public deleteArchivo(file: any) {
    return this.http.delete(this.urlDeleteArchivos + file.id);
  }

  public downloadFiles(files: any) {
    let query = '?';

    for (let i = 0; i < files.length; i++) {
      if (i == files.length - 1) {
        query += `fileIds=${files[i]}`;
      } else {
        query += `fileIds=${files[i]}&`;
      }
    }

    return this.http.get(`${this.urlDownloadFiles}/filess${query}`, {
      responseType: 'blob',
    });
  }

  public downloadFile(fileId) {
    return this.http.get(`${this.urlDownloadFiles}/file/${fileId}`, {
      responseType: 'blob',
    });
  }
}
