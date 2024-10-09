import { Component } from '@angular/core';
import { ApiService, Paciente } from '../../../services/api.service';
import { HttpErrorResponse } from '@angular/common/http';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-registrar-paciente',
  templateUrl: './registrar-paciente.component.html',
  styleUrls: ['./registrar-paciente.component.css']
})
export class RegistrarPacienteComponent {
  nuevoPaciente: Paciente = {
    IdPaciente: 0,
    Nombre: '',
    Dni: '',
    FechaNacimiento: new Date(),
    Estado: 'Registrado',
    FechaRegistro: new Date(),
    SeguridadSocial: '',
    Direccion: '',
    Telefono: '',
    Email: '',
    HistorialMedico: ''
  };

  constructor(private apiService: ApiService) {}

  registerPatient(pacienteForm: NgForm) {
    if (pacienteForm.invalid) {
      Object.values(pacienteForm.controls).forEach(control => {
        control.markAsTouched(); // Marca todos los campos como tocados para mostrar errores si existen
      });
      return;
    }

    // Llamada al API para registrar el paciente
    this.apiService.addPaciente(this.nuevoPaciente).subscribe({
      next: () => {
        this.notificacion = 'Paciente registrado con éxito';
        this.resetForm();
        pacienteForm.resetForm(); // Resetea el formulario después del registro
      },
      error: (error: HttpErrorResponse) => {
        const errorMessage = this.getErrorMessageFromResponse(error);
        alert('Error: ' + errorMessage);
      }
    });
    setTimeout(() => {
      this.notificacion = null;
    }, 2000);
  }


  getErrorMessageFromResponse(error: HttpErrorResponse): string {
    if (error.error instanceof Object) {
      if (error.error.errors) {
        // Si el backend devuelve un objeto de errores en formato de validación, desglosarlo.
        return Object.values(error.error.errors).flat().join(', ');
      }
      return JSON.stringify(error.error);
    } else if (typeof error.error === 'string') {
      return error.error;
    } else {
      return 'Ocurrió un error inesperado.';
    }
  }

  // Validación de DNI (formato 8 dígitos seguidos de una letra)
  isValidDni(dni: string): boolean {
    const dniPattern = /^\d{8}[A-Za-z]$/;
    return dniPattern.test(dni);
  }

  resetForm() {
    this.nuevoPaciente = {
      IdPaciente: 0,
      Nombre: '',
      Dni: '',
      FechaNacimiento: new Date(),
      Estado: 'Registrado',
      FechaRegistro: new Date(),
      SeguridadSocial: '',
      Direccion: '',
      Telefono: '',
      Email: '',
      HistorialMedico: ''
    };
  }
  notificacion: string | null = null;
}
