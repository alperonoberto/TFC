import { Component, OnInit } from '@angular/core';
import { SearchService } from '../services/search/search.service';
import { LoginService } from '../services/login/login.service';
import { RepositorioService } from '../services/repositorio/repositorio.service';
import { MatIcon } from '@angular/material/icon';
import { RelacionesService } from '../services/relaciones/relaciones.service';
import { Router } from '@angular/router';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-user-public',
  templateUrl: './user-public.component.html',
  styleUrls: ['./user-public.component.scss'],
})
export class UserPublicComponent implements OnInit {
  public user: any;
  public userLogged: any;
  public repo: any;
  public userProfilePic: string;
  public listaRelaciones = [];
  public listaRepos = [];
  public isLoading: boolean = true;
  public isSeguido: boolean = false;
  public isHimself: boolean = false;

  constructor(
    private _searchService: SearchService,
    private _loginService: LoginService,
    private _repoService: RepositorioService,
    private _relacionesService: RelacionesService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this._loginService.getUserLoggedIn().subscribe({
      next: res => {
        console.log(res);
        this.userLogged = res;
      },
      error: err => {
        console.log(err);
      }
    })

    this._searchService.userSearched.subscribe({
      next: res => {
        this.user = res;
        this.userProfilePic = res["profilePicture"] == "" ? "./../../../assets/anonimo.jpeg" : res["profilePicture"];
        console.log(res)
        this._repoService.getRepositoriosByUser(this.user['id'])
          .subscribe({
            next: res => {
              this.listaRepos = [res].flat();
              this.checkRelacion();
              this.isLoading = false;
            },
            error: err => {
              console.log(err)
            }
          })
      },
      error: err => {
        console.log(err)
      },
      complete: res => {
        console.log(res)
        
        
      }
    })

  }

  public checkRelacion() {
    this._relacionesService.GetRelaciones().subscribe(
      (res) => {
        console.log(res);
        this.listaRelaciones = [res].flat();

        this.listaRelaciones.forEach((r) => {
          if(this.userLogged.id == this.user.id) {
            this.router.navigateByUrl('/user')
            this.isHimself = true;
          } else if (
            r.seguidorId == this.userLogged.id &&
            r.seguidoId == this.user.id
          ) {
            this.isSeguido = true;
          }
        });
      },
      (err) => {
        console.log(err);
      }
    );
  }

  public seguirUsuario() {
    let relacion = {
      seguidorId: this.userLogged.id,
      seguidoId: this.user.id,
    };
    this._relacionesService.PostRelacion(relacion).subscribe(
      (res) => {
        console.log(res);
      },
      (err) => {
        console.log(err);
      }
    );
    this.isSeguido = true;
  }

  public dejarDeSeguirUsuario() {
    let relacion = {
      id: 0,
      seguidorId: this.userLogged.id,
      seguidoId: this.user.id,
    };

    this.listaRelaciones.forEach((r) => {
      if (r.seguidorId == this.userLogged.id && r.seguidoId == this.user.id) {
        relacion.id = r.id;
      }
    });

    this._relacionesService
      .DeleteRelacion(this.userLogged.id, this.user.id)
      .subscribe(
        (res) => {
          console.log(res);
        },
        (err) => {
          console.log(err);
        }
      );
    this.isSeguido = false;
  }
}
