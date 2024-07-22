import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdministrativoDashboardComponent } from './administrativo-dashboard/administrativo-dashboard.component';
import { AdministrativoRoutingModule } from './administrativo-routing.module';

// Importar el componente standalone
import { HistorialAltasComponent } from '../shared/historial-altas/historial-altas.component';

@NgModule({
  imports: [
    CommonModule,
    AdministrativoRoutingModule,
    HistorialAltasComponent
  ],
  declarations: [
    AdministrativoDashboardComponent
  ]
})
export class AdministrativoModule { }
