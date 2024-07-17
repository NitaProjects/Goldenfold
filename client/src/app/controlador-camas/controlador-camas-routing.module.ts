import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ControladorCamasDashboardComponent } from './controlador-camas-dashboard/controlador-camas-dashboard.component';

const routes: Routes = [
  { path: '', component: ControladorCamasDashboardComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ControladorCamasRoutingModule { }
