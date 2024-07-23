import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: 'historial-altas', 
  loadComponent: () => import('./historial-altas/historial-altas.component')
  .then(m => m.HistorialAltasComponent) },
  { path: 'pacientes', 
  loadComponent: () => import('./pacientes/pacientes.component')
  .then(m => m.PacientesComponent) }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SharedRoutingModule { }
