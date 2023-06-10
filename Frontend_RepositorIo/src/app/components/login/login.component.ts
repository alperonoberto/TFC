import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';

import { LoginService } from '../services/login/login.service';
import { Router } from '@angular/router';
import { AuthComponent } from 'src/app/shared/components/auth/auth.component';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends AuthComponent implements OnInit {
  
  constructor(
    private loginService: LoginService, 
    private router: Router) {
      super();
    }

  loginError: boolean = false;
  isLoading = true

  public listaUsers = [];

  ngOnInit() {
    this.loginService.getUsers()
      .subscribe({
        next: res => {
          this.listaUsers = [res].flat();
        },
        error: err => {
          console.log(err)
        }, complete: () => {
          this.isLoading = false;
        }
      })
  }

  loginForm = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required])
  })

  matcher = new MyErrorStateMatcher();
  
  submit(){
    const user = {
      username: this.loginForm.value.username,
      password: null,
      rol: null
    }
    // this.loginService
    //   .getPasswordEncrypted(this.loginForm.value.password)
    //   .subscribe(
    //     res => {
    //       user.password = res;

    //       for(let i = 0; i < this.listaUsers.length; i++) {
    //         let dbUser = this.listaUsers[i];
      
    //         if(user.username === dbUser["username"] && user.password === dbUser["password"]) {
    //           this.loginService.isLoggedIn.emit(true);
    //           this.loginService.setUserLoggedIn(dbUser);

    //           if(user.username == 'admin') {
    //             this.loginService.isAdmin.emit(true)
    //           }

    //           this.router.navigate(['/home']);
    //         }
    //       }
          
    //       this.loginError = true;
    //       this.loginForm.reset();
    //     },
    //     err => {
    //       console.error(err)
    //     }
    //   );
      this.loginService
      .getPasswordEncrypted(this.loginForm.value.password)
      .subscribe({
        next: res => {
          user.password = res;

          for(let i = 0; i < this.listaUsers.length; i++) {
            let dbUser = this.listaUsers[i];
      
            if(user.username === dbUser["username"] && user.password === dbUser["password"]) {
              this.loginService.isLoggedIn.emit(true);
              this.loginService.setUserLoggedIn(dbUser);

              if(user.username == 'admin') {
                this.loginService.isAdmin.emit(true)
              }

              this.router.navigate(['/home']);
            }
          }
          
          this.loginForm.reset();
        },
        error: err => {
          console.log(err)
          this.loginError = true;
          this.loginForm.reset();
        }
      })
  }

  

}

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}