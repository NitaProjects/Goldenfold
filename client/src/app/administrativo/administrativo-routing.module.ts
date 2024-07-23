import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministrativoDashboardComponent } from './administrativo-dashboard/administrativo-dashboard.component';
import { PacientesComponent } from '../shared/pacientes/pacientes.component';
import { HistorialAltasComponent } from '../shared/historial-altas/historial-altas.component';
import { AsignacionesComponent } from '../shared/asignaciones/asignaciones.component';

const routes: Routes = [
  { path: '', component: AdministrativoDashboardComponent },
  { path: 'pacientes', component: PacientesComponent },
  { path: 'altas', component: HistorialAltasComponent },
  { path: 'asignaciones', component: AsignacionesComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministrativoRoutingModule { }
