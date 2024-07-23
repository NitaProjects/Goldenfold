import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService, Habitacion } from '../../services/api.service';

@Component({
  selector: 'app-habitaciones',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './habitaciones.component.html',
  styleUrls: ['./habitaciones.component.css']
})
export class HabitacionesComponent implements OnInit {
  habitaciones: Habitacion[] = [];
  nuevaHabitacion: Habitacion = { IdHabitacion: 0, Edificio: '', Planta: '', NumeroHabitacion: '' };

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.obtenerHabitaciones();
  }

  obtenerHabitaciones(): void {
    this.apiService.getHabitaciones().subscribe({
      next: (data: Habitacion[]) => {
        this.habitaciones = data;
      },
      error: (error: any) => {
        console.error('Error al obtener las habitaciones', error);
      }
    });
  }

  agregarHabitacion(): void {
    this.apiService.addHabitacion(this.nuevaHabitacion).subscribe({
      next: (habitacion: Habitacion) => {
        this.habitaciones.push(habitacion);
        this.nuevaHabitacion = { IdHabitacion: 0, Edificio: '', Planta: '', NumeroHabitacion: '' }; // Reset form
      },
      error: (error: any) => {
        console.error('Error al agregar la habitación', error);
      }
    });
  }

  borrarHabitacion(id: number): void {
    this.apiService.deleteHabitacion(id).subscribe({
      next: () => {
        this.habitaciones = this.habitaciones.filter(h => h.IdHabitacion !== id);
      },
      error: (error: any) => {
        console.error('Error al borrar la habitación', error);
      }
    });
  }

  actualizarHabitacion(habitacion: Habitacion): void {
    this.apiService.updateHabitacion(habitacion).subscribe({
      next: (habitacionActualizada: Habitacion) => {
        const index = this.habitaciones.findIndex(h => h.IdHabitacion === habitacionActualizada.IdHabitacion);
        if (index !== -1) {
          this.habitaciones[index] = habitacionActualizada;
        }
      },
      error: (error: any) => {
        console.error('Error al actualizar la habitación', error);
      }
    });
  }
}
