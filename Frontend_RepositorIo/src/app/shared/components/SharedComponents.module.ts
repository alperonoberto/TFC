import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WarningModalComponent } from './warningModal/warning-modal.component';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { AuthComponent } from './auth/auth.component';



@NgModule({
  declarations: [
    WarningModalComponent,
    AuthComponent
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule
  ]
})
export class SharedComponentsModule { }
