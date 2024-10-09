import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ControladorCamasDashboardComponent } from './pages/controlador-camas-dashboard/controlador-camas-dashboard.component';
import { ControladorCamasInicioComponent } from './pages/controlador-camas-inicio/controlador-camas-inicio.component';
import { AsignarCamaComponent } from './pages/asignar-cama/asignar-cama.component';
import { EstadoCamasComponent } from './pages/estado-camas/estado-camas.component';
import { SolicitudesIngresoComponent } from './pages/solicitudes-ingreso/solicitudes-ingreso.component';


const routes: Routes = [
  {
    path: '',
    component: ControladorCamasDashboardComponent,  // Dashboard principal con sidebar
    children: [
      { path: 'controlador-camas-inicio', component: ControladorCamasInicioComponent },
      { path: 'asignar-cama', component: AsignarCamaComponent },
      { path: 'estado-camas', component: EstadoCamasComponent },
      { path: 'solicitudes-ingreso', component: SolicitudesIngresoComponent },
      { path: '', redirectTo: 'controlador-camas-inicio', pathMatch: 'full' },  // Página por defecto
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ControladorCamasRoutingModule { }
