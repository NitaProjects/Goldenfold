import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PacientesRegistradosComponent } from './pacientes-registrados/pacientes-registrados.component';
import { HistorialAltasComponent } from './historial-altas/historial-altas.component';

@NgModule({
  imports: [
    CommonModule, 
    FormsModule
  ],
  declarations: [
    PacientesRegistradosComponent,
    HistorialAltasComponent
  ],
  exports: [
    PacientesRegistradosComponent,
    HistorialAltasComponent
  ]
})
export class SharedModule { }
