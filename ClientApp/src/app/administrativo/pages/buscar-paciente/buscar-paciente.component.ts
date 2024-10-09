import { Component } from '@angular/core';
import { ApiService, Paciente, Consulta } from '../../../services/api.service';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar'; // Importa el MatSnackBar

@Component({
  selector: 'app-buscar-paciente',
  templateUrl: './buscar-paciente.component.html',
  styleUrls: ['./buscar-paciente.component.css']
})
export class BuscarPacienteComponent {
  searchName: string = ''; // Nombre del paciente a buscar
  searchSS: string = '';
  pacientesEncontrados: Paciente[] = []; // Resultados de la búsqueda
  errorMensaje: string | null = null; // Mensaje de error
  pacienteSeleccionado: Paciente | null = null; // Paciente seleccionado para consulta o edición
  mostrarFormularioConsulta: boolean = false; // Mostrar u ocultar el formulario de consulta
  mostrarFormularioEdicion: boolean = false; // Mostrar u ocultar el formulario de edición

  consulta: Consulta = {
    IdConsulta: 0,
    IdPaciente: 0,
    IdMedico: 0,
    Motivo: '',
    FechaSolicitud: new Date(),
    FechaConsulta: null,
    Estado: 'pendiente'
  };

  constructor(private apiService: ApiService, private snackBar: MatSnackBar) { } // Inyecta el MatSnackBar

  buscarPaciente(event: Event) {
    event.preventDefault();

    this.errorMensaje = null;
    this.pacientesEncontrados = [];

    if (this.searchName.trim() !== '' || this.searchSS.trim() !== '') {
      this.apiService.getPacientes(this.searchName, this.searchSS).subscribe({
        next: (pacientes: Paciente[]) => {
          if (pacientes.length > 0) {
            this.pacientesEncontrados = pacientes;
          } else {
            this.snackBar.open('No se encontraron pacientes con los datos proporcionados.', 'Cerrar', {
              duration: 3000,
              panelClass: ['snack-bar-error']
            });
          }
        },
        error: (error: HttpErrorResponse) => {
          this.snackBar.open('Error al buscar pacientes. Inténtelo más tarde.', 'Cerrar', {
            duration: 3000,
            panelClass: ['snack-bar-error']
          });
        }
      });
    } else {
      this.snackBar.open('Por favor, ingrese un nombre o número de seguridad social para la búsqueda.', 'Cerrar', {
        duration: 3000,
        panelClass: ['snack-bar-warning']
      });
    }
  }

  editarPaciente(paciente: Paciente) {
    if (this.pacienteSeleccionado && this.pacienteSeleccionado.IdPaciente === paciente.IdPaciente) {
      this.pacienteSeleccionado = null;
      this.mostrarFormularioEdicion = false;
    } else {
      this.pacienteSeleccionado = { ...paciente };
      this.mostrarFormularioEdicion = true;
      this.mostrarFormularioConsulta = false;
    }
  }

  actualizarPaciente() {
    if (this.pacienteSeleccionado) {
      this.apiService.updatePaciente(this.pacienteSeleccionado).subscribe({
        next: () => {
          this.buscarPaciente(new Event(''));
          this.pacienteSeleccionado = null;
          this.mostrarFormularioEdicion = false;
          this.snackBar.open('Paciente actualizado exitosamente.', 'Cerrar', {
            duration: 3000,
            panelClass: ['snack-bar-success']
          });
        },
        error: (error: HttpErrorResponse) => {
          this.snackBar.open('Error al actualizar el paciente. Inténtelo más tarde.', 'Cerrar', {
            duration: 3000,
            panelClass: ['snack-bar-error']
          });
        }
      });
    }
  }

  abrirFormularioConsulta(paciente: Paciente) {
    if (paciente.Estado === 'EnConsulta') {
      this.snackBar.open('El paciente ya está en consulta.', 'Cerrar', {
        duration: 3000,
        panelClass: ['snack-bar-warning']
      });
      return;
    }

    if (this.pacienteSeleccionado && this.pacienteSeleccionado.IdPaciente === paciente.IdPaciente && this.mostrarFormularioConsulta) {
      this.pacienteSeleccionado = null;
      this.mostrarFormularioConsulta = false;
    } else {
      this.pacienteSeleccionado = paciente;
      this.mostrarFormularioConsulta = true;
      this.mostrarFormularioEdicion = false;

      this.consulta.IdPaciente = paciente.IdPaciente;
      this.consulta.FechaSolicitud = new Date();
      this.consulta.Estado = 'pendiente';
    }
  }

  registrarConsulta() {
    if (this.consulta.IdMedico && this.consulta.Motivo) {
      this.apiService.addConsulta(this.consulta).subscribe({
        next: () => {
          this.mostrarFormularioConsulta = false;
          this.pacienteSeleccionado = null;
          this.resetConsulta();
          this.snackBar.open('Consulta registrada exitosamente.', 'Cerrar', {
            duration: 3000,
            panelClass: ['snack-bar-success'],
            horizontalPosition: 'center',
            verticalPosition: 'top'
          });
        },
        error: (error: HttpErrorResponse) => {
          this.snackBar.open('Error al registrar la consulta. Inténtelo más tarde.', 'Cerrar', {
            duration: 3000,
            panelClass: ['snack-bar-error']
          });
        }
      });
    } else {
      this.snackBar.open('Por favor, ingrese todos los datos requeridos.', 'Cerrar', {
        duration: 3000,
        panelClass: ['snack-bar-warning']
      });
    }
  }

  cancelarEdicion() {
    this.pacienteSeleccionado = null;
    this.mostrarFormularioEdicion = false;
  }

  cancelarConsulta() {
    this.mostrarFormularioConsulta = false;
    this.resetConsulta();
  }

  resetConsulta() {
    this.consulta = {
      IdConsulta: 0,
      IdPaciente: 0,
      IdMedico: 0,
      Motivo: '',
      FechaSolicitud: new Date(),
      FechaConsulta: null,
      Estado: 'pendiente'
    };
  }
}
