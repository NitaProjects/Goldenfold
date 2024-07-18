import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministrativoDashboardComponent } from './administrativo-dashboard/administrativo-dashboard.component';
import { PacientesRegistradosComponent } from '../shared/pacientes-registrados/pacientes-registrados.component';
import { PacientesIngresadosComponent } from '../shared/pacientes-ingresados/pacientes-ingresados.component';
import { HistorialAltasComponent } from '../shared/historial-altas/historial-altas.component';

const routes: Routes = [
  { path: '', component: AdministrativoDashboardComponent },
  { path: 'pacientes', component: PacientesRegistradosComponent },
  { path: 'altas', component: HistorialAltasComponent },
  { path: 'consultar-ingresados', component: PacientesIngresadosComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministrativoRoutingModule { }
