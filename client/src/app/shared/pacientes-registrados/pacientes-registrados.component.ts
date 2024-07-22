// src/app/shared/pacientes-registrados/pacientes-registrados.component.ts
import { Component, OnInit } from '@angular/core';
import { ApiService, Paciente } from '../../services/api.service';

@Component({
  selector: 'app-pacientes-registrados',
  templateUrl: './pacientes-registrados.component.html',
  styleUrls: ['./pacientes-registrados.component.css']
})
export class PacientesRegistradosComponent implements OnInit {
  pacientes: Paciente[] = [];

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.apiService.getPacientes().subscribe({
      next: (data: Paciente[]) => {
        this.pacientes = data;
      },
      error: (error: any) => {
        console.error('Error al obtener los datos de pacientes', error);
      }
    });
  }

  editarRegistro(id: number) {
    console.log(`Editar registro con ID ${id}`);
    // Aquí puedes agregar la lógica para editar el registro
  }

  copiarRegistro(id: number) {
    console.log(`Copiar registro con ID ${id}`);
    // Aquí puedes agregar la lógica para copiar el registro
  }

  borrarRegistro(id: number) {
    this.apiService.deletePaciente(id).subscribe({
      next: () => {
        console.log(`Borrar registro con ID ${id}`);
        this.pacientes = this.pacientes.filter(paciente => paciente.iD_Paciente !== id);
      },
      error: (error: any) => {
        console.error('Error al borrar el paciente', error);
      }
    });
  }
}
