import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdministrativoDashboardComponent } from './administrativo-dashboard/administrativo-dashboard.component';
import { SharedModule } from '../shared/shared.module';
import { AdministrativoRoutingModule } from './administrativo-routing.module';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    AdministrativoRoutingModule
  ],
  declarations: [
    AdministrativoDashboardComponent
  ]
})
export class AdministrativoModule { }
