import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { LoginService } from '../services/login/login.service';
import { AuthComponent } from 'src/app/shared/components/auth/auth.component';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent extends AuthComponent {

  constructor(
    private router: Router, 
    private loginService: LoginService) { 
      super();
    }

  isLoggedIn: boolean = false;
  isLoggedInRegister: boolean = this.loginService.isLoggedInRegister;
  isAdmin: boolean = false;

  ngOnInit(): void {
    this.loginService.isLoggedIn.subscribe(data => {
      this.isLoggedIn = data;
      console.log(this.isLoggedIn)
    });

    this.loginService.isAdmin.subscribe(
      res => {
        console.log(res)
        this.isAdmin = res;
      },
      err => {

      }
    )
  }

  logOut() {
    let user = {}
    this.isLoggedIn = false;
    this.loginService.setUserLoggedIn(user)
    this.redirectHome();
  }

  redirectHome() {
    this.router.navigate(['/home']);
  }
}
