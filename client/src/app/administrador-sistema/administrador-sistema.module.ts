import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdministradorSistemaRoutingModule } from './administrador-sistema-routing.module';
import { AdministradorSistemaDashboardComponent } from './administrador-sistema-dashboard/administrador-sistema-dashboard.component';

@NgModule({
  declarations: [
    AdministradorSistemaDashboardComponent  // Declarar el componente aquí
  ],
  imports: [
    CommonModule,
    FormsModule,
    AdministradorSistemaRoutingModule
  ]
})
export class AdministradorSistemaModule { }
