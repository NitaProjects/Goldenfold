import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'historial-altas',
    loadComponent: () => import('./historial-altas/historial-altas.component')
      .then(m => m.HistorialAltasComponent)
  },
  {
    path: 'pacientes',
    loadComponent: () => import('./pacientes/pacientes.component')
      .then(m => m.PacientesComponent)
  },
  {
    path: 'asignaciones',
    loadComponent: () => import('./asignaciones/asignaciones.component')
      .then(m => m.AsignacionesComponent)
  },
  {
    path: 'usuarios',
    loadComponent: () => import('./usuarios/usuarios.component')
      .then(m => m.UsuariosComponent)
  },
  {
    path: 'habitaciones',
    loadComponent: () => import('./habitaciones/habitaciones.component')
      .then(m => m.HabitacionesComponent)
  },
  {
    path: 'camas',
    loadComponent: () => import('./camas/camas.component')
      .then(m => m.CamasComponent)
  },
  {
    path: 'roles',
    loadComponent: () => import('./roles/roles.component')
      .then(m => m.RolesComponent)
  },
  {
    path: 'ingresos',
    loadComponent: () => import('./ingresos/ingresos.component')
      .then(m => m.IngresosComponent)
  },
  {
    path: 'consultas',
    loadComponent: () => import('./consultas/consultas.component')
      .then(m => m.ConsultasComponent)
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SharedRoutingModule { }
