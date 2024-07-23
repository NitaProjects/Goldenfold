import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService, Cama } from '../../services/api.service';

@Component({
  selector: 'app-camas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './camas.component.html',
  styleUrls: ['./camas.component.css']
})
export class CamasComponent implements OnInit {
  camas: Cama[] = [];
  nuevaCama: Cama = { Ubicacion: '', Estado: '', Tipo: '' };

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.obtenerCamas();
  }

  obtenerCamas(): void {
    this.apiService.getCamas().subscribe({
      next: (data: Cama[]) => {
        this.camas = data;
      },
      error: (error: any) => {
        console.error('Error al obtener las camas', error);
      }
    });
  }

  agregarCama(): void {
    this.apiService.addCama(this.nuevaCama).subscribe({
      next: (cama: Cama) => {
        this.camas.push(cama);
        this.nuevaCama = { Ubicacion: '', Estado: '', Tipo: '' }; 
      },
      error: (error: any) => {
        console.error('Error al agregar la cama', error);
      }
    });
  }

  actualizarCama(cama: Cama): void {
    this.apiService.updateCama(cama).subscribe({
      next: (camaActualizada: Cama) => {
        const index = this.camas.findIndex(c => c.Ubicacion === camaActualizada.Ubicacion);
        if (index !== -1) {
          this.camas[index] = camaActualizada;
        }
      },
      error: (error: any) => {
        console.error('Error al actualizar la cama', error);
      }
    });
  }

  borrarCama(Ubicacion: string): void {
    this.apiService.deleteCama(Ubicacion).subscribe({
      next: () => {
        this.camas = this.camas.filter(c => c.Ubicacion !== Ubicacion);
      },
      error: (error: any) => {
        console.error('Error al borrar la cama', error);
      }
    });
  }
}
