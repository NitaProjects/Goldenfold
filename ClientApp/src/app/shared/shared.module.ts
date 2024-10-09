import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// Angular Material Imports
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatButtonModule } from '@angular/material/button';
import { SharedRoutingModule } from './shared-routing.module';
import { SearchBoxComponent } from './search-box/search-box.component';

// Angular Material Imports

import { MatDialogModule, MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBarModule } from '@angular/material/snack-bar';
// Componentes personalizados
import { DialogFormularioComponent } from './dialog-formulario/dialog-formulario.component';



@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedRoutingModule,

    // Angular Material Modules
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
  ],
  declarations: [
    SearchBoxComponent,
    DialogFormularioComponent,
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    // Exportación de componentes
    SearchBoxComponent,
    DialogFormularioComponent, // Exportar si se va a usar fuera del módulo

    // Exportación de Angular Material Modules
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatButtonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSnackBarModule, // Incluido en exportaciones para su uso fuera del módulo
  ],
  providers: [
    { provide: MAT_DIALOG_DATA, useValue: {} },
    { provide: MatDialogRef, useValue: {} },
  ],
})
export class SharedModule { }
