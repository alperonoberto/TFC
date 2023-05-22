import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BASE_URL } from 'src/app/environment/environment.constants';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  public urlUsersPost = BASE_URL + 'users/add';
  public user: any;

  constructor(private _http: HttpClient) { }

  public postUser(user) {
    return this._http.post(this.urlUsersPost, user);
  }

  public getUserLoggedIn() {
    return this.user;
  }
}
