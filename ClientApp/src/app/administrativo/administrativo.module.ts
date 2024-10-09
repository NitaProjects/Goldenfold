import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdministrativoDashboardComponent } from './pages/administrativo-dashboard/administrativo-dashboard.component';
import { AdministrativoRoutingModule } from './administrativo-routing.module';

// Componentes específicos de administrativo
import { RegistrarPacienteComponent } from './pages/registrar-paciente/registrar-paciente.component';
import { BuscarPacienteComponent } from './pages/buscar-paciente/buscar-paciente.component';
import { AdministrativoInicioComponent } from './pages/administrativo-inicio/administrativo-inicio.component';
import { PacientesComponent } from './pages/pacientes/pacientes.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    AdministrativoRoutingModule,
    MatSnackBarModule
  ],
  declarations: [
    AdministrativoDashboardComponent,
    RegistrarPacienteComponent,
    BuscarPacienteComponent,
    AdministrativoInicioComponent,
  ],
  
})
export class AdministrativoModule { }
