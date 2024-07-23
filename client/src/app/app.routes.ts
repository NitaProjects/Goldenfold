import { Routes } from '@angular/router';
import { InicioComponent } from './inicio/inicio.component';


export const routes: Routes = [
  // Redirige al componente de inicio
  { path: '', redirectTo: '/inicio', pathMatch: 'full' }, 
  // Componente de inicio
  { path: 'inicio', component: InicioComponent }, 
  
  // Lazy loading del módulo shared
  { path: 'shared',
  loadChildren: () => import('./shared/shared.module').then(m => m.SharedModule) },
  // Lazy loading del módulo médico
  { path: 'medico-dashboard', 
  loadChildren: () => import('./medico/medico.module').then(m => m.MedicoModule) }, 
  // Lazy loading del módulo administrativo
  { path: 'administrativo-dashboard', 
  loadChildren: () => import('./administrativo/administrativo.module').then(m => m.AdministrativoModule) }, 
  // Lazy loading del módulo controlador de camas
  { path: 'controlador-camas-dashboard', 
  loadChildren: () => import('./controlador-camas/controlador-camas.module').then(m => m.ControladorCamasModule) }, 
  // Lazy loading del módulo administrador del sistema
  { path: 'administrador-sistema-dashboard', 
  loadChildren: () => import('./administrador-sistema/administrador-sistema.module').then(m => m.AdministradorSistemaModule) }
];
