import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OverviewComponent } from './components/cars/overview/overview.component';
import { AddComponent } from './components/cars/add/add.component';
import { HomeComponent } from './components/cars/home/home.component';
import { DetailsComponent } from './components/cars/details/details.component';
import { LoginComponent } from './components/user/login/login.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'add', component: AddComponent },
  { path: 'login', component: LoginComponent },
  { path: 'overview', component: OverviewComponent },
  { path: 'details/:id', component: DetailsComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
