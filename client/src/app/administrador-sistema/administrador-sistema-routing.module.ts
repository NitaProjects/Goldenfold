import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministradorSistemaDashboardComponent } from './administrador-sistema-dashboard/administrador-sistema-dashboard.component';
import { PacientesComponent } from '../shared/pacientes/pacientes.component';
import { HistorialAltasComponent } from '../shared/historial-altas/historial-altas.component';
import { AsignacionesComponent } from '../shared/asignaciones/asignaciones.component';
import { RolesComponent } from '../shared/roles/roles.component';
import { UsuariosComponent } from '../shared/usuarios/usuarios.component';
import { HabitacionesComponent } from '../shared/habitaciones/habitaciones.component';
import { CamasComponent } from '../shared/camas/camas.component';

const routes: Routes = [
  { path: '', component: AdministradorSistemaDashboardComponent },
  { path: 'pacientes', component: PacientesComponent },
  { path: 'altas', component: HistorialAltasComponent },
  { path: 'asignaciones', component: AsignacionesComponent },
  { path: 'roles', component: RolesComponent },
  { path: 'usuarios', component: UsuariosComponent },
  { path: 'habitaciones', component: HabitacionesComponent },
  { path: 'camas', component: CamasComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministradorSistemaRoutingModule { }
