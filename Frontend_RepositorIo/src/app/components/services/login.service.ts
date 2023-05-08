import { EventEmitter, Injectable, Output } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor() { }

  @Output() isLoggedIn: EventEmitter<boolean> = new EventEmitter();

  
}
