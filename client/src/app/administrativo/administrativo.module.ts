import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdministrativoRoutingModule } from './administrativo-routing.module';
import { AdministrativoDashboardComponent } from './administrativo-dashboard/administrativo-dashboard.component';

@NgModule({
  declarations: [
    AdministrativoDashboardComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    AdministrativoRoutingModule
  ]
})
export class AdministrativoModule { }
