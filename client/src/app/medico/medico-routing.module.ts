// medico-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MedicoDashboardComponent } from './medico-dashboard/medico-dashboard.component';

const routes: Routes = [
  { path: '', component: MedicoDashboardComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MedicoRoutingModule { }
