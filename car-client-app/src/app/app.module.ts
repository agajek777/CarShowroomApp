import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from "@angular/common/http";

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AddComponent } from './components/cars/add/add.component';
import { DeleteComponent } from './components/cars/delete/delete.component';
import { DetailsComponent } from './components/cars/details/details.component';
import { OverviewComponent } from './components/cars/overview/overview.component';
import { UpdateComponent } from './components/cars/update/update.component';

@NgModule({
  declarations: [
    AppComponent,
    AddComponent,
    DeleteComponent,
    DetailsComponent,
    OverviewComponent,
    UpdateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
