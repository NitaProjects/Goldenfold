import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Paciente } from '../../services/api.service';

@Component({
  selector: 'app-dialog-formulario',
  templateUrl: './dialog-formulario.component.html',
  styleUrls: ['./dialog-formulario.component.css']
})
export class DialogFormularioComponent {

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Paciente, 
    public dialogRef: MatDialogRef<DialogFormularioComponent>  
  ) {}

  // Método para manejar el envío del formulario
  guardar(): void {
    this.dialogRef.close(this.data);  
  }
}
