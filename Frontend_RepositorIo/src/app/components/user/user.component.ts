import { Component, OnInit } from '@angular/core';
import { RegisterService } from '../services/register/register.service';
import { LoginService } from '../services/login/login.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {
  constructor(private _registerService: RegisterService, private _loginService: LoginService) {}

  public user: any;
  
  ngOnInit() {
    this._loginService.getUserLoggedIn()
      .subscribe(
        res => {this.user = res; console.log(res)}, err => {console.log(err)}
      );
  }

  

}
