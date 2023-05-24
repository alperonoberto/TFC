import { Component, OnInit } from '@angular/core';
import { RegisterService } from '../services/register/register.service';
import { LoginService } from '../services/login/login.service';
import { RepositorioService } from '../services/repositorio/repositorio.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  constructor(
    private _registerService: RegisterService, 
    private _loginService: LoginService,
    private _repositorioService: RepositorioService) {}

  public user: any;
  public listaRepos: any[] = []
  
  ngOnInit() {
    this._loginService.getUserLoggedIn()
      .subscribe(
        res => {
          this.user = res

          this._repositorioService.getRepositorioByUser(this.user.id)
        .subscribe(
          res => {
            console.log(this.listaRepos)
            console.log(res)
            // this.listaRepos = [res];
          },
          err => {
            console.error('Error GET repositorios')
          }
        )
        }, 
        err => {
          console.log(err)
        }
      );

    
  }

  

}
