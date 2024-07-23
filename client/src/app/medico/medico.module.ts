// medico.module.ts
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MedicoDashboardComponent } from './medico-dashboard/medico-dashboard.component';
import { MedicoRoutingModule } from './medico-routing.module';

@NgModule({
  declarations: [
    MedicoDashboardComponent
  ],
  imports: [
    CommonModule,
    MedicoRoutingModule
  ]
})
export class MedicoModule { }
