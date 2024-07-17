import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ControladorCamasRoutingModule } from './controlador-camas-routing.module';
import { ControladorCamasDashboardComponent } from './controlador-camas-dashboard/controlador-camas-dashboard.component';

@NgModule({
  declarations: [
    ControladorCamasDashboardComponent  
  ],
  imports: [
    CommonModule,
    FormsModule,
    ControladorCamasRoutingModule
  ]
})
export class ControladorCamasModule { }
