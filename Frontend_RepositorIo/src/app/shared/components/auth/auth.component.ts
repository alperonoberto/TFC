import { Component } from '@angular/core';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent {
  public checkStorage(): boolean {
    const usuario = sessionStorage.getItem("usuario");
    console.log(usuario)
    return usuario ? true : false;
  }

  public setStorage(user) {
    sessionStorage.setItem("usuario", JSON.stringify(user))
  }

  public removeStorage() {
    sessionStorage.removeItem("usuario")
  }
}
