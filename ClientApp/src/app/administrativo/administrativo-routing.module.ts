import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegistrarPacienteComponent } from './pages/registrar-paciente/registrar-paciente.component';
import { BuscarPacienteComponent } from './pages/buscar-paciente/buscar-paciente.component';
import { AdministrativoDashboardComponent } from './pages/administrativo-dashboard/administrativo-dashboard.component';
import { PacientesComponent } from './pages/pacientes/pacientes.component';

import { AdministrativoInicioComponent } from './pages/administrativo-inicio/administrativo-inicio.component';

const routes: Routes = [
  {
    path: '',
    component: AdministrativoDashboardComponent,
    children: [
      { path: 'administrativo-inicio', component: AdministrativoInicioComponent },
      { path: 'registrar-paciente', component: RegistrarPacienteComponent },
      { path: 'buscar-paciente', component: BuscarPacienteComponent },
      { path: 'pacientes', component: PacientesComponent },
      { path: '', redirectTo: 'administrativo-inicio', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministrativoRoutingModule { }
