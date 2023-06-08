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

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
})
export class UserComponent implements OnInit {
  constructor(
    private _archivoService: ArchivoService,
    private _loginService: LoginService,
    private _relacionService: RelacionesService,
    private _repositorioService: RepositorioService,
    private router: Router
  ) {}

  public user: any;
  public listaRepos: any[] = [];
  public listaRelaciones: any[] = [];
  public listaSeguidores: any[] = [];
  public listaSeguidos: any[] = [];
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
    this._repositorioService.getRepositoriosByUser(this.user.id).subscribe(
      (res) => {
        this.listaRepos = [...[res]].flat();
      },
      (err) => {
        console.error('Error GET repositorios');
      }
    );

    this._relacionService.GetRelacionesByUser(this.user.id).subscribe(
      (res) => {
        console.log(res);
        this.listaRelaciones = [res].flat();

        this.listaSeguidores = this.listaRelaciones.filter((r) => {
          r.seguidoId === this.user.id;
        });

        this.listaSeguidos = this.listaRelaciones.filter((r) => {
          r.seguidorId === this.user.id;
        });
      },
      (err) => {
        console.log(err);
      }
    );
  }

  public openRepo() {
    this.router.navigate(['repositories']);
  }

  public editarPerfil() {
    this.isEditing = true;

    this.userForm.patchValue({
      username: this.user.username,
    });
  }

  public submitChanges() {
    // let file = new FormData;
    // file.append('file', this.fileSelected);

    // this._archivoService.postProfilePic(file, this.user.Id)
    //   .subscribe(
    //     res => {
    //       console.log(res)
    //     },
    //     err => {
    //       console.log(err)
    //     }
    //   )

    this.user.username = this.userForm.get('username').value;

    this._loginService.updateUser(this.user).subscribe(
      (res) => {
        console.log(res);
      },
      (err) => {
        console.log(err);
      }
    );
  }

  onFileSelected(event: any) {
    this.fileSelected = event.target.files[0];
    this.base64String = this.getBase64(this.fileSelected);

    // this.base64String = Base64.stringify(Utf8.parse(this.fileSelected));

    console.log(this.fileSelected);
    console.log(this.base64String);
  }

  getBase64(file): string {
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
      console.log(reader.result);
      return reader.result;
    };
    return '';
  }


}
