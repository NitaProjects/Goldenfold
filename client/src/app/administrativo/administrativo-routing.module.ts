import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministrativoDashboardComponent } from './administrativo-dashboard/administrativo-dashboard.component';

const routes: Routes = [
  { path: '', component: AdministrativoDashboardComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministrativoRoutingModule { }
