import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { RegisterService } from '../services/register/register.service';
import { Router } from '@angular/router';
import { LoginService } from '../services/login/login.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  constructor(private _registerService: RegisterService, public _loginService: LoginService, protected router: Router) {}

  registerForm = new FormGroup({
    username: new FormControl("", [Validators.required]),
    email: new FormControl("", [Validators.required, Validators.email]),
    password: new FormControl("", [Validators.required]),
    passwordConfirmation: new FormControl("", [Validators.required])
  })

  submit() {
      let user = {
        username: this.registerForm.get('username').value,
        password: this.registerForm.get('password').value,
        email: this.registerForm.get('email').value,
        rol: this.registerForm.get('username').value == 'admin' ? 'ADMIN' : 'USER'
      }
      this._registerService.postUser(user)
        .subscribe(
          res => {
            console.log(res);
            this._loginService.isLoggedIn.emit(true);
            this.router.navigate(['/home']);
          },
          error => {
            console.error('Error POST usuarios');
          }
        );
    this.registerForm.reset();
  }

  

}
