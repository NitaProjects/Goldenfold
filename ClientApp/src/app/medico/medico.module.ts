import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MedicoRoutingModule } from './medico-routing.module';

import { MedicoDashboardComponent } from './pages/medico-dashboard/medico-dashboard.component';
import { MedicoInicioComponent } from './pages/medico-inicio/medico-inicio.component';
import { BuscarPacienteComponent } from './pages/buscar-paciente/buscar-paciente.component';
import { ConsultasProgramadasComponent } from './pages/consultas-programadas/consultas-programadas.component';
import { IngresosComponent } from './pages/ingresos/ingresos.component';

@NgModule({
  declarations: [
    MedicoDashboardComponent,
    MedicoInicioComponent,
    BuscarPacienteComponent,
    ConsultasProgramadasComponent,
    IngresosComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    MedicoRoutingModule
  ]
})
export class MedicoModule { }
