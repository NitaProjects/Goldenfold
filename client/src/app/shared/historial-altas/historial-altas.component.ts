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
    iD_Historial: 0,
    iD_Paciente: 0,
    fecha_Alta: new Date(),
    diagnostico: '',
    tratamiento: ''
  };

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.apiService.getHistorialAltas().subscribe({
      next: (data: HistorialAlta[]) => {
        this.historialAltas = data;
      },
      error: (error: any) => {
        console.error('Error al obtener los datos de historial de altas', error);
      }
    });
  }

  agregarHistorialAlta() {
    this.apiService.addHistorialAlta(this.nuevoHistorialAlta).subscribe({
      next: (nuevoHistorialAlta: HistorialAlta) => {
        this.historialAltas.push(nuevoHistorialAlta);
        this.nuevoHistorialAlta = {
          iD_Historial: 0,
          iD_Paciente: 0,
          fecha_Alta: new Date(),
          diagnostico: '',
          tratamiento: ''
        };
      },
      error: (error: any) => {
        console.error('Error al agregar el historial de alta', error);
      }
    });
  }

  borrarHistorialAlta(id: number) {
    this.apiService.deleteHistorialAlta(id).subscribe({
      next: () => {
        this.historialAltas = this.historialAltas.filter(historialAlta => historialAlta.iD_Historial !== id);
      },
      error: (error: any) => {
        console.error('Error al borrar el historial de alta', error);
      }
    });
  }

  actualizarHistorialAlta(id: number, historialAlta: HistorialAlta) {
    this.apiService.updateHistorialAlta(id, historialAlta).subscribe({
      next: (historialAltaActualizado: HistorialAlta) => {
        const index = this.historialAltas.findIndex(ha => ha.iD_Historial === id);
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
