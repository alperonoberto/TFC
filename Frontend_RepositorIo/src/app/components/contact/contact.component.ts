import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent {
  contactForm = new FormGroup({
    autor: new FormControl(""),
    asunto: new FormControl(""),
    descripcion: new FormControl(""),
  });

  submit() {}
}
