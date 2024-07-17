// medico-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MedicoDashboardComponent } from './medico-dashboard/medico-dashboard.component';
import { PacientesComponent } from './pacientes/pacientes.component'; // Importar el PacientesComponent en el mismo nivel

const routes: Routes = [
  { path: '', component: MedicoDashboardComponent },
  { path: 'pacientes', component: PacientesComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MedicoRoutingModule { }
