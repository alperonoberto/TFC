import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { LoginService } from '../services/login/login.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent {

  constructor(private router: Router, private loginService: LoginService) { }

  isLoggedIn: boolean = false;
  isLoggedInRegister: boolean = this.loginService.isLoggedInRegister;

  ngOnInit(): void {
    this.loginService.isLoggedIn.subscribe(data => {
      this.isLoggedIn = data;
    })
  }

  logOut() {
    this.isLoggedIn = false;
    this.redirectHome();
  }

  redirectHome() {
    this.router.navigate(['/home']);
  }
}
