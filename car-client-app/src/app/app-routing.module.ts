import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { OverviewComponent } from "./components/cars/overview/overview.component";
import { AddComponent } from "./components/cars/add/add.component";
import { DetailsComponent } from "./components/cars/details/details.component";
import { UpdateComponent } from "./components/cars/update/update.component";
import { DeleteComponent } from "./components/cars/delete/delete.component";

const routes: Routes = [
  {path: '', component: OverviewComponent},
  {path: 'add', component: AddComponent},
  {path: 'edit/:id', component: DetailsComponent},
  {path: 'update/:id', component: UpdateComponent},
  {path: 'delete/:id', component: DeleteComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
