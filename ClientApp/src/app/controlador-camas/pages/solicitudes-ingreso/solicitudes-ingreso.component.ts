import { Component, OnInit } from '@angular/core';
import { ApiService, Ingreso, HistorialAlta, Cama, Asignacion} from '../../../services/api.service';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-solicitudes-ingreso',
  templateUrl: './solicitudes-ingreso.component.html',
  styleUrls: ['./solicitudes-ingreso.component.css']
})
export class SolicitudesIngresoComponent implements OnInit {
  solicitudesPendientes: Ingreso[] = [];
  errorMensaje: string | null = null;
  solicitudSeleccionada: Ingreso | null = null;
  formularioSeleccionado: 'alta' | 'seleccionarCama' | null = null;
  camasDisponibles: Cama[] = [];
  camaSeleccionada: number | null = null;

  // Datos del formulario "Dar de Alta"
  diagnostico: string = '';
  tratamiento: string = '';

  constructor(private apiService: ApiService) { }

  ngOnInit() {
    this.obtenerSolicitudesPendientes();
  }

  obtenerSolicitudesPendientes() {
    this.apiService.getIngresos(undefined, undefined, 'Pendiente').subscribe({
      next: (solicitudes: Ingreso[]) => {
        if (solicitudes.length > 0) {
          this.solicitudesPendientes = solicitudes;
          this.errorMensaje = null; // Limpiar mensaje de error si hay solicitudes
        } else {
          this.solicitudesPendientes = [];
          this.errorMensaje = 'No hay solicitudes de ingreso pendientes.'; // Mostrar mensaje si no hay solicitudes
        }
      },
      error: (error: HttpErrorResponse) => {
        if (error.status === 404) {
          this.errorMensaje = 'No hay solicitudes de ingreso pendientes.';
        } else {
          this.errorMensaje = 'Error al cargar las solicitudes de ingreso. Por favor, inténtalo de nuevo.';
        }
        this.solicitudesPendientes = [];
      }
    });
  }

  // Seleccionar una solicitud para realizar acciones
  addAsignacion(solicitud: Ingreso) {
    this.solicitudSeleccionada = solicitud;
    this.formularioSeleccionado = 'seleccionarCama'; 
    this.obtenerCamasDisponibles(solicitud.TipoCama);
  }


  // Mostrar el formulario de "Dar de Alta" o "Seleccionar Cama"
  mostrarFormulario(tipo: 'alta' | 'seleccionarCama') {
    this.formularioSeleccionado = tipo;
  }

  // Dar de alta al paciente
  darDeAlta() {
    if (!this.solicitudSeleccionada) {
      console.error('No hay solicitud seleccionada');
      return;
    }

    // Crear el objeto de historial de alta
    const historialAlta: HistorialAlta = {
      IdHistorial: 0,
      IdPaciente: this.solicitudSeleccionada.IdPaciente,
      IdMedico: this.solicitudSeleccionada.IdMedico,
      Diagnostico: this.diagnostico,
      Tratamiento: this.tratamiento,
      FechaAlta: new Date()
    };

    // Crear historial de alta y actualizar el estado de la solicitud
    this.apiService.addHistorialAlta(historialAlta).subscribe({
      next: (response) => {
        window.alert('Paciente dado de alta correctamente');
        this.resetFormulario();
        this.obtenerSolicitudesPendientes();
      },
      error: (err) => {
        window.alert('Hubo un error al dar de alta al paciente.');
      }
    });
  }

  obtenerCamasDisponibles(tipoCama: string) {
    // Hacer una llamada a las asignaciones activas (sin fecha de liberación)
    this.apiService.getAsignaciones(undefined, undefined, new Date(), null, undefined).subscribe({
      next: (asignaciones) => {
        // Llamar a las camas disponibles
        this.apiService.getCamas().subscribe({
          next: (camas: Cama[]) => {
            // Filtrar las camas que no están asignadas
            const camasOcupadas = asignaciones.map(asignacion => asignacion.IdCama);
            this.camasDisponibles = camas.filter(cama => 
              !camasOcupadas.includes(cama.IdCama) && cama.Tipo === tipoCama
            );
          },
          error: (error: HttpErrorResponse) => {
            console.error('Error al obtener las camas disponibles:', error.message);
          }
        });
      },
      error: (error: HttpErrorResponse) => {
        console.error('Error al obtener las asignaciones activas:', error.message);
      }
    });
  }
  
  
  

  // Confirmar la asignación de una cama
  confirmarCama() {
    if (!this.solicitudSeleccionada || !this.camaSeleccionada) {
      console.error('No hay solicitud o cama seleccionada');
      return;
    }

    this.apiService.addAsignacion({
      IdAsignacion: 0,
      IdPaciente: this.solicitudSeleccionada!.IdPaciente,
      IdCama: this.camaSeleccionada!,
      FechaAsignacion: new Date(),
      FechaLiberacion: null,
      AsignadoPor: 7
    }).subscribe({
      next: (response) => {
        window.alert('Cama asignada correctamente');
        this.resetFormulario();
        this.obtenerSolicitudesPendientes();
      },
      error: (err) => {
        if (err.error && err.error.message) {
          window.alert(`Error: ${err.error.message}`);
        } else if (err.error) {
          window.alert(`Error: ${err.error}`);
        } else {
          window.alert('Hubo un error al asignar la cama.');
        }
      }
    });
  }

  resetFormulario() {
    this.solicitudSeleccionada = null;
    this.formularioSeleccionado = null;
    this.diagnostico = '';
    this.tratamiento = '';
    this.camaSeleccionada = null;
  }
}
