import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministradorSistemaDashboardComponent } from './administrador-sistema-dashboard/administrador-sistema-dashboard.component';

const routes: Routes = [
  { path: '', component: AdministradorSistemaDashboardComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministradorSistemaRoutingModule { }
