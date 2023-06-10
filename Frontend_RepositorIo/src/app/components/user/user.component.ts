import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login/login.service';
import { RepositorioService } from '../services/repositorio/repositorio.service';
import { Router } from '@angular/router';
import { RelacionesService } from '../services/relaciones/relaciones.service';
import { FormControl, FormGroup } from '@angular/forms';
import { ArchivoService } from '../services/archivo/archivo.service';
// import * as crypto from 'crypto-js';
import Base64 from 'crypto-js/enc-base64';
import Utf8 from 'crypto-js/enc-utf8';
import { SearchService } from '../services/search/search.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
})
export class UserComponent implements OnInit {
  constructor(
    private _loginService: LoginService,
    private _relacionService: RelacionesService,
    private _repositorioService: RepositorioService,
    private _searchService: SearchService,
    private router: Router
  ) {}

  public user: any;
  public listaRepos: any[] = [];
  public listaRelaciones: any[] = [];
  public listaRelacionesSeguidores: any[] = [];
  public listaSeguidores: any[] = [];
  public isEditing: boolean = false;
  public srcResult: string = './../../../assets/anonimo.jpeg';
  public base64String: string;
  public fileSelected: any;

  public userForm = new FormGroup({
    username: new FormControl(''),
    foto: new FormControl(''),
  });

  ngOnInit() {
    this.user = this._loginService.user;
    this.user.profilePicture.length > 0
      ? (this.srcResult = this.user.profilePicture)
      : null;

    this._repositorioService.getRepositoriosByUser(this.user.id).subscribe({
      next: (res) => {
        this.listaRepos = [res].flat();
      },
      error: (err) => {
        console.log(err);
      },
    });

    this._relacionService.GetRelacionesByUser(this.user.id).subscribe({
      next: (res) => {
        this.listaRelaciones = [res].flat();

        this.listaRelaciones.forEach((r) => {
          if (r.seguidoId == this.user.id) {
            this.listaRelacionesSeguidores.push(r);
          }
        });
      },
      error: (err) => {
        console.log(err);
      },
      complete: () => {
        this.listaRelacionesSeguidores.forEach((seguidor) => {
          this._loginService.getUserById(seguidor.seguidorId).subscribe({
            next: (res) => {
              console.log(res);
              this.listaSeguidores.push(res);
            },
          });
        });
      },
    });
  }

  public openRepo() {
    this.router.navigate(['repositories']);
  }

  public mostrarUserPage(username: string) {
    this._loginService.getUserByUsername(username).subscribe({
      next: (res) => {
        this._searchService.userSearched.emit(res);
      },
      error: (err) => {
        console.log(err);
      },
      complete: () => {
        this.router.navigateByUrl('public/user');
      },
    });
  }

  public editarPerfil() {
    this.isEditing = true;

    this.userForm.patchValue({
      username: this.user.username,
      foto: this.user.profilePicture,
    });
  }

  public submitChanges() {
    this.user.username = this.userForm.get('username').value;
    this.userForm.get('foto').pristine
      ? (this.user.profilePicture = this.base64String)
      : null;

    console.log(this.srcResult)
    console.log(this.userForm.get('foto'))
    console.log(this.base64String)

    this._loginService.updateUser(this.user).subscribe({
      next: (res) => {
        this.isEditing = false;
        this.srcResult = this.base64String;
        document
          .querySelector('.profileImage')
          .setAttribute('src', this.srcResult);
        this.router.navigateByUrl('user');
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  onFileSelected(event: any) {
    this.fileSelected = event.target.files[0];
    this.getBase64(this.fileSelected).then(
      (res) => {
        this.base64String = res;
      },
      (err) => {
        console.log(err);
      }
    );
  }

  getBase64(file: File): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onload = () => {
        if (typeof reader.result === 'string') {
          this.base64String = reader.result;
          resolve(this.base64String);
        } else {
          reject(new Error('Invalid result type. Expected a string.'));
        }
      };
      reader.onerror = (error) => {
        reject(error);
      };
      reader.readAsDataURL(file);
    });
  }
}
