import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable, Output } from '@angular/core';
import { BASE_URL } from 'src/app/environment/index';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private urlUsersGet = BASE_URL + 'users/';
  private urlUsersGetById = BASE_URL + 'users/';
  private urlUsersPost = BASE_URL + 'users/add';
  private urlUsersPut = BASE_URL + 'users/update';
  private urlUsersDelete = BASE_URL + 'users/delete/';
  private urlUsersEncrypt = BASE_URL + 'users/encrypt';
  private urlUsersGetByUsername = BASE_URL + 'users/username/';

  constructor(private _http: HttpClient) { }

  public isLoggedInRegister: boolean;
  @Output() isLoggedIn: EventEmitter<boolean> = new EventEmitter();
  @Output() loginError: EventEmitter<boolean> = new EventEmitter();
  @Output() isAdmin: EventEmitter<boolean> = new EventEmitter();

  public user: any;


  public getUsers() {
    return this._http.get(this.urlUsersGet);
  }

  public getUserById(id: number) {
    return this._http.get(this.urlUsersGetById + id);
  }

  public getUserByUsername(username: string) {
    return this._http.get(this.urlUsersGetByUsername + username);
  }

  public getUserLoggedIn() {
    return this.getUserById(this.user.id);
  }

  public setUserLoggedIn(user) {
    this.user = user;
  }

  public getPasswordEncrypted(value: string) {
    return this._http.get(`${this.urlUsersEncrypt}?login=${value}`);
  }

  public deleteUser(user) {
    return this._http.delete(this.urlUsersDelete + user.id);
  }

  public updateUser(user) {
    return this._http.put(this.urlUsersPut, user)
  }
  
}
