import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { BASE_URL } from 'src/app/environment/index';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  public urlUsersGet = BASE_URL + 'users/';
  public urlUsersGetById = BASE_URL + 'users/';
  public urlUsersPost = BASE_URL + 'users/add';
  public urlUsersPut = BASE_URL + 'users/update';
  public urlUsersDelete = BASE_URL + 'users/delete';
  public urlUsersEncrypt = BASE_URL + 'users/encrypt';

  constructor(private _http: HttpClient) { }

  public isLoggedInRegister: boolean;
  @Output() isLoggedIn: EventEmitter<boolean> = new EventEmitter();
  @Output() loginError: EventEmitter<boolean> = new EventEmitter();

  public user: any;


  public getUsers() {
    return this._http.get(this.urlUsersGet);
  }

  public getUserById(id: number) {
    return this._http.get(this.urlUsersGetById + id);
  }

  public getUserLoggedIn() {
    return this.getUserById(this.user.id);
  }

  public setUserLoggedIn(user) {
    this.user = user;
  }

  public getPasswordEncrypted(password: string) {
    return this._http.get(`${this.urlUsersEncrypt}?login=${password}`);
  }
  
}
