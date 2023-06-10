import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { LoginService } from '../services/login/login.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  
  contactForm = new FormGroup({
    autor: new FormControl(""),
    asunto: new FormControl(""),
    descripcion: new FormControl(""),
  });

  public isLoading = true

  constructor(
    protected _loginService: LoginService
  ) {}

  public ngOnInit(): void {
    this._loginService.getUserLoggedIn()
      .subscribe(
        res => {
          console.log(res)
          this.contactForm.patchValue({
            autor: res['email']
          })

          this.isLoading = false
        },
        err => {
          console.log(err)
        }
      )
  }

  submit() {}
}
