import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService, Asignacion } from '../../services/api.service';

@Component({
  selector: 'app-asignaciones',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './asignaciones.component.html',
  styleUrls: ['./asignaciones.component.css']
})
export class AsignacionesComponent implements OnInit {
  asignaciones: Asignacion[] = [];
  nuevaAsignacion: Asignacion = {
    IdAsignacion: 0,
    IdPaciente: 0,
    Ubicacion: '',
    FechaAsignacion: new Date(),
    FechaLiberacion: new Date(),
    AsignadoPor: 0
  };

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.obtenerAsignaciones();
  }

  obtenerAsignaciones(): void {
    this.apiService.getAsignaciones().subscribe({
      next: (data: Asignacion[]) => {
        this.asignaciones = data;
      },
      error: (error: any) => {
        console.error('Error al obtener las asignaciones', error);
      }
    });
  }

  agregarAsignacion(): void {
    this.apiService.addAsignacion(this.nuevaAsignacion).subscribe({
      next: (asignacion: Asignacion) => {
        this.asignaciones.push(asignacion);
        this.nuevaAsignacion = {
          IdAsignacion: 0,
          IdPaciente: 0,
          Ubicacion: '',
          FechaAsignacion: new Date(),
          FechaLiberacion: new Date(),
          AsignadoPor: 0
        }; // Reset form
      },
      error: (error: any) => {
        console.error('Error al agregar la asignación', error);
      }
    });
  }

  actualizarAsignacion(asignacion: Asignacion): void {
    this.apiService.updateAsignacion(asignacion).subscribe({
      next: (asignacionActualizada: Asignacion) => {
        const index = this.asignaciones.findIndex(a => a.IdAsignacion === asignacionActualizada.IdAsignacion);
        if (index !== -1) {
          this.asignaciones[index] = asignacionActualizada;
        }
      },
      error: (error: any) => {
        console.error('Error al actualizar la asignación', error);
      }
    });
  }

  borrarAsignacion(id: number): void {
    this.apiService.deleteAsignacion(id).subscribe({
      next: () => {
        this.asignaciones = this.asignaciones.filter(a => a.IdAsignacion !== id);
      },
      error: (error: any) => {
        console.error('Error al borrar la asignación', error);
      }
    });
  }
}
