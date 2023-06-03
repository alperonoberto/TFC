import { Component, OnInit } from '@angular/core';
import { RegisterService } from '../services/register/register.service';
import { LoginService } from '../services/login/login.service';
import { RepositorioService } from '../services/repositorio/repositorio.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
})
export class UserComponent implements OnInit {
  constructor(
    private _registerService: RegisterService,
    private _loginService: LoginService,
    private _repositorioService: RepositorioService,
    private router: Router
  ) {}

  public user: any;
  public listaRepos: any[] = [];

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
  }

  openRepo() {
    this.router.navigate(['repositories'])
  }

}
