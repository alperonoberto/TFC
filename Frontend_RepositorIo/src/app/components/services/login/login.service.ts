import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { BASE_URL } from 'src/app/environment/index';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  public urlUsersGet = BASE_URL + 'users/';
  public urlUsersPost = BASE_URL + 'users/add';
  public urlUsersPut = BASE_URL + 'users/update';
  public urlUsersDelete = BASE_URL + 'users/delete';

  constructor(private _http: HttpClient) { }

  @Output() isLoggedIn: EventEmitter<boolean> = new EventEmitter();
  @Output() loginError: EventEmitter<boolean> = new EventEmitter();


  public getUsers() {
    return this._http.get(this.urlUsersGet);
  }
  
}
