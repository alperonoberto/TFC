import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FooterComponent } from './footer/footer.component';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { RouterModule } from '@angular/router';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { HttpClientModule } from '@angular/common/http';
import { MatListModule } from '@angular/material/list';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';

import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ContactComponent } from './contact/contact.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NotFoundComponent } from './not-found/not-found.component';
import { RepositoriosComponent } from './repositorios/repositorios.component';
import { UserComponent } from './user/user.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { SearchbarComponent } from './searchbar/searchbar.component';
import { UserPublicComponent } from './userPublic/user-public.component';
import { AdminComponent } from './admin/admin.component';

@NgModule({
  declarations: [
    ToolbarComponent,
    FooterComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    ContactComponent,
    NotFoundComponent,
    RepositoriosComponent,
    UserComponent,
    SearchbarComponent,
    UserPublicComponent,
    AdminComponent,
  ],
  imports: [
    CommonModule,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    RouterModule,
    MatCardModule,
    ReactiveFormsModule,
    MatGridListModule,
    HttpClientModule,
    MatCheckboxModule,
    MatListModule,
    MatAutocompleteModule,
    MatTableModule,
    MatPaginatorModule,
  ],
  exports: [
    ToolbarComponent,
    FooterComponent,
    HomeComponent,
    LoginComponent,
    RegisterComponent,
    ContactComponent,
    UserComponent,
  ],
  providers: [HttpClientModule],
})
export class ComponentsModule {}
