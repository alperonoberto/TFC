import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WarningModalComponent } from './warningModal/warning-modal.component';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';



@NgModule({
  declarations: [
    WarningModalComponent
  ],
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule
  ]
})
export class SharedComponentsModule { }
