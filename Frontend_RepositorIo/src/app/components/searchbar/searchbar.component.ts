import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login/login.service';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable, map, startWith } from 'rxjs';

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

  constructor(protected _loginService: LoginService) {}

  public ngOnInit() {
    this.cargarUsers();
    this.filteredUsers = this.searchbar.valueChanges.pipe(
      startWith(''),
      map(value => this._filter(value || '')),
    );
  }


  private cargarUsers(): void {
    this._loginService.getUsers().subscribe(
      (res) => {
        this.usersList = [...[res]].flat();
        this.usersList.forEach(user => {
          this.usernamesList.push(user.username);
        })
        
      },
      (err) => {
        console.log(err);
      }
    );
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.usernamesList.filter(user => user.toLowerCase().includes(filterValue));
  }


}
