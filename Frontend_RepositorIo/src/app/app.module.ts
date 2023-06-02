import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ComponentsModule } from './components/components.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { WarningModalComponent } from './shared/components/warningModal/warning-modal.component';
import { MatCardModule } from '@angular/material/card';
import { SharedComponentsModule } from './shared/components/sharedComponents.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ComponentsModule,
    BrowserAnimationsModule,
    MatDialogModule,
    MatCardModule,
    SharedComponentsModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
  entryComponents: [WarningModalComponent],
})
export class AppModule {}
