import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService, HistorialAlta } from '../../services/api.service';

@Component({
  selector: 'app-historial-altas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './historial-altas.component.html',
  styleUrls: ['./historial-altas.component.css']
})
export class HistorialAltasComponent implements OnInit {
  historialAltas: HistorialAlta[] = [];
  nuevoHistorialAlta: HistorialAlta = {
    IdHistorial: 0,
    IdPaciente: 0,
    FechaAlta: new Date(),
    Diagnostico: '',
    Tratamiento: ''
  };

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.obtenerHistorialAltas();
  }

  obtenerHistorialAltas(): void {
    this.apiService.getHistorialAltas().subscribe({
      next: (data: HistorialAlta[]) => {
        this.historialAltas = data;
      },
      error: (error: any) => {
        console.error('Error al obtener los datos de historial de altas', error);
      }
    });
  }

  agregarHistorialAlta(): void {
    this.apiService.addHistorialAlta(this.nuevoHistorialAlta).subscribe({
      next: (nuevoHistorialAlta: HistorialAlta) => {
        this.historialAltas.push(nuevoHistorialAlta);
        this.nuevoHistorialAlta = {
          IdHistorial: 0,
          IdPaciente: 0,
          FechaAlta: new Date(),
          Diagnostico: '',
          Tratamiento: ''
        };
      },
      error: (error: any) => {
        console.error('Error al agregar el historial de alta', error);
      }
    });
  }

  borrarHistorialAlta(id: number): void {
    this.apiService.deleteHistorialAlta(id).subscribe({
      next: () => {
        this.historialAltas = this.historialAltas.filter(historialAlta => historialAlta.IdHistorial !== id);
      },
      error: (error: any) => {
        console.error('Error al borrar el historial de alta', error);
      }
    });
  }

  actualizarHistorialAlta(historialAlta: HistorialAlta): void {
    this.apiService.updateHistorialAlta(historialAlta).subscribe({
      next: (historialAltaActualizado: HistorialAlta) => {
        const index = this.historialAltas.findIndex(ha => ha.IdHistorial === historialAltaActualizado.IdHistorial);
        if (index !== -1) {
          this.historialAltas[index] = historialAltaActualizado;
        }
      },
      error: (error: any) => {
        console.error('Error al actualizar el historial de alta', error);
      }
    });
  }
}
