// medico.module.ts
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MedicoDashboardComponent } from './medico-dashboard/medico-dashboard.component';
import { PacientesComponent } from './pacientes/pacientes.component'; // Importar PacientesComponent
import { MedicoRoutingModule } from './medico-routing.module';

@NgModule({
  declarations: [
    MedicoDashboardComponent,
    PacientesComponent // Declarar PacientesComponent
  ],
  imports: [
    CommonModule,
    MedicoRoutingModule
  ]
})
export class MedicoModule { }
