import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MedicoDashboardComponent } from './pages/medico-dashboard/medico-dashboard.component';
import { BuscarPacienteComponent } from './pages/buscar-paciente/buscar-paciente.component';
import { MedicoInicioComponent } from './pages/medico-inicio/medico-inicio.component';
import { IngresosComponent } from './pages/ingresos/ingresos.component';
import { ConsultasProgramadasComponent } from './pages/consultas-programadas/consultas-programadas.component';

const routes: Routes = [
  {
    path: '',
    component: MedicoDashboardComponent,  // Dashboard principal con sidebar
    children: [
      { path: 'medico-inicio', component: MedicoInicioComponent },
      { path: 'ingresos', component: IngresosComponent },
      { path: 'buscar-paciente', component: BuscarPacienteComponent },  // Carga dentro del dashboard
      { path: 'consultas-programadas', component: ConsultasProgramadasComponent },
      { path: '', redirectTo: 'medico-inicio', pathMatch: 'full' }, // Página por defecto
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class MedicoRoutingModule { }
 