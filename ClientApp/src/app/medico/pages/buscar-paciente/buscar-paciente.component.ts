import { Component } from '@angular/core';
import { ApiService, Paciente, Consulta } from '../../../services/api.service';
import { HttpErrorResponse } from '@angular/common/http';

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

  // Inicializar un objeto vacío para la nueva consulta
  consulta: Consulta = {
    IdConsulta: 0,
    IdPaciente: 0,
    IdMedico: 0,
    Motivo: '',
    FechaSolicitud: new Date(),
    FechaConsulta: null,
    Estado: 'pendiente'
  };

  constructor(private apiService: ApiService) { }

  buscarPaciente(event: Event) {
    event.preventDefault();

    // Si ya hay resultados de pacientes y se vuelve a ejecutar la búsqueda, ocultar el formulario
    if (this.pacientesEncontrados.length > 0) {
      this.pacientesEncontrados = [];
      this.errorMensaje = null;
      return;
    }

    this.errorMensaje = null;
    this.pacientesEncontrados = [];

    // Verificar si al menos uno de los campos de búsqueda está lleno
    if (this.searchName.trim() !== '' || this.searchSS.trim() !== '') {
      this.apiService.getPacientes(this.searchName, this.searchSS).subscribe({
        next: (pacientes: Paciente[]) => {
          if (pacientes.length > 0) {
            // Si ambos campos (nombre y SS) están llenos, filtrar los resultados
            if (this.searchName.trim() !== '' && this.searchSS.trim() !== '') {
              this.pacientesEncontrados = pacientes.filter(paciente =>
                paciente.Nombre.toLowerCase() === this.searchName.toLowerCase() &&
                paciente.SeguridadSocial === this.searchSS
              );
            } else {
              // Si solo uno de los campos está lleno, mostrar todos los resultados
              this.pacientesEncontrados = pacientes;
            }

            // Si no se encontraron coincidencias después del filtrado
            if (this.pacientesEncontrados.length === 0) {
              this.errorMensaje = 'No se encontraron pacientes que coincidan con el nombre y número de seguridad social proporcionados.';
            }
          } else {
            this.errorMensaje = 'No se encontraron pacientes con ese nombre o número de seguridad social.';
          }
        },
        error: (error: HttpErrorResponse) => {
          this.errorMensaje = 'Error al buscar el paciente. Por favor, inténtalo de nuevo.';
        }
      });
    } else {
      this.errorMensaje = 'Por favor, ingresa un nombre o un número de seguridad social para buscar.';
    }
  }

  // Seleccionar un paciente para editar
  editarPaciente(paciente: Paciente) {
    if (this.pacienteSeleccionado && this.pacienteSeleccionado.IdPaciente === paciente.IdPaciente) {
      // Si ya está seleccionado, ocultar el formulario
      this.pacienteSeleccionado = null;
      this.mostrarFormularioEdicion = false;
    } else {
      // Si no está seleccionado, mostrar el formulario de edición
      this.pacienteSeleccionado = { ...paciente }; // Clona el paciente seleccionado
      this.mostrarFormularioEdicion = true; // Mostrar el formulario de edición
      this.mostrarFormularioConsulta = false; // Ocultar el formulario de consulta
    }
  }


  // Actualizar los datos del paciente
  actualizarPaciente() {
    if (this.pacienteSeleccionado) {
      this.apiService.updatePaciente(this.pacienteSeleccionado).subscribe({
        next: () => {
          this.buscarPaciente(new Event('')); // Rehacer la búsqueda para actualizar los datos
          this.pacienteSeleccionado = null; // Limpiar el paciente seleccionado
          this.mostrarFormularioEdicion = false; // Ocultar el formulario de edición
          this.notificacion = 'Paciente actualizado con éxito';
        },
        error: (error: HttpErrorResponse) => {
          this.errorMensaje = 'Error al actualizar el paciente. Por favor, inténtalo de nuevo.';
        }
      });
    }
    setTimeout(() => {
      this.notificacion = null;
    }, 2000);
  }
  // Método para cancelar la edición del paciente
  cancelarEdicion() {
    this.pacienteSeleccionado = null; // Limpiar el paciente seleccionado
    this.mostrarFormularioEdicion = false; // Ocultar el formulario de edición
  }
  notificacion: string | null = null;
}
