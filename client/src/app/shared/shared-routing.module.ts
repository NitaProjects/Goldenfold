import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'historial-altas', 
  loadComponent: () => import('./historial-altas/historial-altas.component')
  .then(m => m.HistorialAltasComponent) },
  { path: 'pacientes-registrados', 
  loadComponent: () => import('./pacientes-registrados/pacientes-registrados.component')
  .then(m => m.PacientesRegistradosComponent) }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SharedRoutingModule { }
