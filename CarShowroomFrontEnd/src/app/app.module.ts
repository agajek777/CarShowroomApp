import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MainNavComponent } from './main-nav/main-nav.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatDialogModule } from "@angular/material/dialog";
import { MatInputModule } from "@angular/material/input";
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDatepickerModule } from "@angular/material/datepicker";
import { AddComponent } from "./components/cars/add/add.component";
import { DetailsComponent } from './components/cars/details/details.component';
import { OverviewComponent } from './components/cars/overview/overview.component';
import { HomeComponent } from './components/cars/home/home.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { DialogComponent } from './components/cars/details/dialog/dialog.component';
import { FormComponent } from './components/cars/add/form/form.component';
import { ReactiveFormsModule } from "@angular/forms";
import { MatFormFieldModule } from '@angular/material/form-field';
import { LoginComponent } from './components/user/login/login.component';
import { LoaderComponent } from './components/shared/loader/loader.component';
import { LoaderService } from './services/loader.service';
import { LoaderInterceptor } from './models/loader-interceptor';
import { EditComponent } from './components/cars/edit/edit.component';

@NgModule({
  declarations: [
    AppComponent,
    MainNavComponent,
    AddComponent,
    DetailsComponent,
    OverviewComponent,
    HomeComponent,
    DialogComponent,
    FormComponent,
    LoginComponent,
    LoaderComponent,
    EditComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    HttpClientModule,
    MatDialogModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatProgressSpinnerModule
  ],
  providers: [
    LoaderService,
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
