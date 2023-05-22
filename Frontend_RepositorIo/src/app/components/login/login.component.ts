import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';

import { LoginService } from '../services/login/login.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  
  constructor(private loginService: LoginService, private router: Router) {}

  loginError: boolean = false;

  public listaUsers = [];

  ngOnInit() {
    this.loginService.getUsers()
      .subscribe(
        res => {
          this.listaUsers = [res].flat();
        },
        err => {
          console.error('Error GET usuarios');
        }
      )
  }

  loginForm = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required])
  })

  matcher = new MyErrorStateMatcher();
  
  submit(){
    for(let i = 0; i < this.listaUsers.length; i++) {
      let user = this.listaUsers[i];
      if(this.loginForm.value.username === user["username"] && this.loginForm.value.password === user["password"]) {
        this.loginService.isLoggedIn.emit(true);
        this.loginService.setUserLoggedIn(user);
        this.router.navigate(['/home']);
        
      }
    }
      this.loginError = true;

    this.loginForm.reset();
  }

  

}

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}