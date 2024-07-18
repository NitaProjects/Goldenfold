import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PacientesRegistradosComponent } from './pacientes-registrados/pacientes-registrados.component';
import { PacientesIngresadosComponent } from './pacientes-ingresados/pacientes-ingresados.component';
import { HistorialAltasComponent } from './historial-altas/historial-altas.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    PacientesRegistradosComponent,
    PacientesIngresadosComponent,
    HistorialAltasComponent
  ],
  exports: [
    PacientesRegistradosComponent,
    PacientesIngresadosComponent,
    HistorialAltasComponent
  ]
})
export class SharedModule { }
