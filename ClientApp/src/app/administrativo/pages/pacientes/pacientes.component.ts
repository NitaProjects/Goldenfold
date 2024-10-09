import { Component, OnInit,ViewChild } from '@angular/core';
import { ApiService, Paciente } from '../../../services/api.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { SnackbarComponent } from '../../../shared/snackbar/snackbar.component';

@Component({
  selector: 'app-pacientes',
  standalone: true,
  imports: [CommonModule, FormsModule,SnackbarComponent],
  templateUrl: './pacientes.component.html',
  styleUrls: ['./pacientes.component.css'],
})
export class PacientesComponent implements OnInit {

  pacientes: Paciente[] = [];
  pacientesPaginados: Paciente[] = [];
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
    HistorialMedico: '',
  };
  pacienteSeleccionado: Paciente | null = null;

  mostrarDetallesPaciente(paciente: Paciente): void {
    this.pacienteSeleccionado = paciente;
  }

  cerrarPopup(): void {
    this.pacienteSeleccionado = null;
  }

  pacienteParaActualizar: Paciente | null = null;

  paginaActual: number = 1;
  pacientesPorPagina: number = 7;
  totalPaginas: number = 0;

  filtro: string = '';  // Filtro combinado para nombre y DNI
  orden: 'asc' | 'desc' = 'asc';
  columnaOrdenada: keyof Paciente | '' = '';

  constructor(private apiService: ApiService) {}
  @ViewChild(SnackbarComponent) snackbar!: SnackbarComponent;
  ngOnInit(): void {
    this.obtenerPacientes();
  }

  obtenerPacientes(): void {
    this.apiService.getPacientes().subscribe({
      next: (data: Paciente[]) => {
        this.pacientes = data;
        this.filtrarPacientes();
      },
      error: (error: any) => {
        console.error('Error al obtener los pacientes', error);
      },
    });
  }

  filtrarPacientes(): void {
    // Filtra la lista de pacientes buscando coincidencias con el filtro de nombre o DNI
    let pacientesFiltrados = this.pacientes.filter((paciente) =>
      paciente.Nombre.toLowerCase().includes(this.filtro.toLowerCase()) ||
      paciente.Dni.includes(this.filtro)
    );

    // Si hay una columna seleccionada para ordenar
    if (this.columnaOrdenada) {
      pacientesFiltrados.sort((a, b) => {
        const valorA = a[this.columnaOrdenada as keyof Paciente];
        const valorB = b[this.columnaOrdenada as keyof Paciente];

        if (valorA < valorB) {
          return this.orden === 'asc' ? -1 : 1;
        }
        if (valorA > valorB) {
          return this.orden === 'asc' ? 1 : -1;
        }
        return 0;
      });
    }

    // Actualiza la paginación
    const inicio = (this.paginaActual - 1) * this.pacientesPorPagina;
    const fin = inicio + this.pacientesPorPagina;
    this.pacientesPaginados = pacientesFiltrados.slice(inicio, fin);

    // Calcula el total de páginas
    this.totalPaginas = Math.ceil(
      pacientesFiltrados.length / this.pacientesPorPagina
    );

    // Verifica si la página actual tiene datos, si no, redirige a la última página con datos
    if (this.pacientesPaginados.length === 0 && this.paginaActual > 1) {
      this.paginaActual--;
      this.filtrarPacientes(); // Llama nuevamente para ajustar la paginación
    }
  }

  paginaAnterior(): void {
    if (this.paginaActual > 1) {
      this.paginaActual--;
      this.filtrarPacientes();
    }
  }
  // Método para ir a la primera página
irAPrimeraPagina(): void {
  this.paginaActual = 1;
  this.filtrarPacientes();
}

// Método para ir a la última página
irALaUltimaPagina(): void {
  this.paginaActual = this.totalPaginas;
  this.filtrarPacientes();
}

  paginaSiguiente(): void {
    if (this.paginaActual < this.totalPaginas) {
      this.paginaActual++;
      this.filtrarPacientes();
    }
  }

  ordenar(columna: keyof Paciente): void {
    if (this.columnaOrdenada === columna) {
      this.orden = this.orden === 'asc' ? 'desc' : 'asc';
    } else {
      this.columnaOrdenada = columna;
      this.orden = 'asc';
    }
    this.filtrarPacientes();
  }

  agregarPaciente(): void {
    this.apiService.addPaciente(this.nuevoPaciente).subscribe(
      (paciente: Paciente) => {
        // Manejar el paciente agregado exitosamente
        this.pacientes.push(paciente);
        this.mostrarFormularioAgregar = false;

        // Restablecer el formulario
        this.resetForm();

        // Recargar la lista de pacientes después de agregar
        this.obtenerPacientes();

        // Mostrar la notificación de éxito después de un breve retraso
        setTimeout(() => {
          this.snackbar.showNotification("success", "Paciente agregado con éxito!");
        }, 2000); // Espera 2 segundos antes de mostrar la notificación
      },
      (err) => {
        // Manejar errores
        console.error('Error al agregar paciente', err);
        this.snackbar.showNotification("error", "Error al agregar paciente"); // Notificación de error
      }
    );
  }

  registerPatient(event: Event) {
    event.preventDefault();

    // Validaciones antes de enviar al servidor
    if (this.nuevoPaciente.SeguridadSocial.length !== 12) {
      alert('El número de seguridad social debe tener 12 dígitos.');
      return;
    }

    if (!this.isValidDni(this.nuevoPaciente.Dni)) {
      alert('El DNI debe tener 8 números seguidos de una letra.');
      return;
    }

    if (this.nuevoPaciente.FechaNacimiento > new Date()) {
      alert('La fecha de nacimiento no puede ser en el futuro.');
      return;
    }

    if (this.nuevoPaciente.Telefono && !this.isValidPhoneNumber(this.nuevoPaciente.Telefono)) {
      alert('El número de teléfono debe tener exactamente 9 dígitos.');
      return;
    }

    // Llamada al API para registrar el paciente
    this.apiService.addPaciente(this.nuevoPaciente).subscribe({
      next: () => {
        // Recargar la página o la lista de pacientes después de agregar
        this.obtenerPacientes(); // o usa window.location.reload();

        // Restablecer el formulario y cerrar el formulario de agregar
        this.resetForm();
        this.mostrarFormularioAgregar = false;

        // Mostrar la notificación de éxito después de un breve retraso
        setTimeout(() => {
          this.snackbar.showNotification("success", "Paciente agregado con éxito!");
        }, 300);
      },
      error: (error: HttpErrorResponse) => {
        const errorMessage = this.getErrorMessageFromResponse(error);
        alert('Error: ' + errorMessage);
      }
    });
  }


  getErrorMessageFromResponse(error: HttpErrorResponse): string {
    if (error.error instanceof Object) {
      if (error.error.errors) {
        // Si el backend devuelve un objeto de errores en formato de validación, desglosarlo.
        return Object.values(error.error.errors).flat().join(', ');
      }
      // En caso de otro formato de error, devolver todos los valores del objeto.
      return JSON.stringify(error.error);
    } else if (typeof error.error === 'string') {
      // Si el error es una cadena de texto, devolverlo tal cual.
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

  // Validación de número de teléfono (9 dígitos)
  isValidPhoneNumber(phone: string): boolean {
    const phonePattern = /^\d{9}$/;
    return phonePattern.test(phone);
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

  mostrarFormularioActualizar: boolean = false;

  toggleActualizarPaciente(paciente: Paciente): void {
    if (
      this.pacienteParaActualizar &&
      this.pacienteParaActualizar.IdPaciente === paciente.IdPaciente
    ) {
      this.pacienteParaActualizar = null;
      this.mostrarFormularioActualizar = false; // Cerrar el formulario
    } else {
      this.pacienteParaActualizar = { ...paciente };
      this.mostrarFormularioActualizar = true; // Abrir el formulario
    }
  }

  actualizarPaciente(): void {
    if (this.pacienteParaActualizar) {
      this.apiService.updatePaciente(this.pacienteParaActualizar).subscribe({
        next: (pacienteActualizado: Paciente) => {
          this.obtenerPacientes();

          this.pacienteParaActualizar = null;
          this.mostrarFormularioActualizar = false;
          this.snackbar.showNotification("success", "Paciente actualizado con éxito!"); // Notificación de éxito
        },
        error: (error: any) => {
          console.error('Error al actualizar el paciente', error);
          this.snackbar.showNotification("error", "Error al actualizar el paciente"); // Notificación de error
        },
      });
    }


    // Ocultar la notificación después de 2 segundos
    setTimeout(() => {
      this.notificacion = null;
    }, 2000);
  }

  cerrar() {
    window.close();
  }

  aplicarFiltro(filtro: string): void {  // Método combinado para nombre y DNI
    this.filtro = filtro;
    this.filtrarPacientes();
  }

  mostrarFormularioAgregar: boolean = false;

  // Método para alternar la visibilidad del formulario de agregar paciente
  toggleFormularioAgregar(): void {
    this.mostrarFormularioAgregar = !this.mostrarFormularioAgregar;
  }

  mostrarMas: boolean = false;

  // Método para alternar la visibilidad de la información adicional
  toggleMostrarMas(): void {
    this.mostrarMas = !this.mostrarMas;
  }

  notificacion: string | null = null;
}
