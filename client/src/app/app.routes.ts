import { Routes } from '@angular/router';
import { InicioComponent } from './inicio/inicio.component';
import { HistorialAltasComponent } from './shared/historial-altas/historial-altas.component';


export const routes: Routes = [
  { path: '', redirectTo: '/inicio', pathMatch: 'full' },
  { path: 'inicio', component: InicioComponent },
  { path: 'medico-dashboard', loadChildren: () => import('./medico/medico.module').then(m => m.MedicoModule) },
  { path: 'administrativo-dashboard', loadChildren: () => import('./administrativo/administrativo.module').then(m => m.AdministrativoModule) },
  { path: 'controlador-camas-dashboard', loadChildren: () => import('./controlador-camas/controlador-camas.module').then(m => m.ControladorCamasModule) },
  { path: 'administrador-sistema-dashboard', loadChildren: () => import('./administrador-sistema/administrador-sistema.module').then(m => m.AdministradorSistemaModule) },
  { path: 'historial-altas', component: HistorialAltasComponent },
  { path: '', redirectTo: 'historial-altas', pathMatch: 'full' }
];
