import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { LoginService } from '../services/login/login.service';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';
import { Router } from '@angular/router';
import { SearchService } from '../services/search/search.service';

@Component({
  selector: 'app-searchbar',
  templateUrl: './searchbar.component.html',
  styleUrls: ['./searchbar.component.scss'],
})
export class SearchbarComponent implements OnInit {

  public usersList = [];
  public usernamesList = [];
  public filteredUsers: Observable<string[]>;
  public searchbar = new FormControl('');
  public currentUser: string;

  constructor(
    protected _loginService: LoginService,
    protected _searchService: SearchService,
    protected router: Router
    ) {}

  public ngOnInit() {
    this.cargarUsers();
    this.filteredUsers = this.searchbar.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value || '')),
    );
  }


  private cargarUsers(): void {
    // this._loginService.getUsers().subscribe(
    //   (res) => {
    //     this.usersList = [...[res]].flat();
    //     this.usersList.forEach(user => {
    //       this.usernamesList.push(user.username);
    //     })
        
    //   },
    //   (err) => {
    //     console.log(err);
    //   }
    // );
    this._loginService.getUsers()
      .subscribe({
        next: res => {
          this.usersList = [res].flat();
          this.usersList.forEach(user => {
            this.usernamesList.push(user.username)
          })
        },
        error: err => {
          console.log(err)
        },
        complete: () => {}
      })
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.usernamesList.filter(user => user.toLowerCase().includes(filterValue));
  }

  public async mostrarUserPage(user: string) {

    this._loginService.getUserByUsername(user)
      .subscribe({
        next: res => {
          this._searchService.userSearched.emit(res)
          this.router.navigateByUrl("public/user")
        },
        error: err => {
          console.log(err)
        },
        complete: () => {
        }
      })
  }

  public selectUser(user: string) {
    this.currentUser = user;
    this.mostrarUserPage(this.currentUser);
  }


}
