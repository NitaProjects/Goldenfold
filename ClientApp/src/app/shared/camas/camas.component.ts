import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService, Cama } from '../../services/api.service';
import { HttpBackend } from '@angular/common/http';

@Component({
  selector: 'app-camas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './camas.component.html',
  styleUrls: ['./camas.component.css']
})
export class CamasComponent implements OnInit {
  camas: Cama[] = [];
  camasFiltradas: Cama[] = [];

  nuevaCama: Cama = {
    IdCama: 0,
    Ubicacion: '',
    Estado: '',
    Tipo: '',
    IdHabitacion: 0
  };
  camaParaActualizar: Cama | null = null;

  paginaActual: number = 1;
  camasPorPagina: number = 10;
  totalPaginas: number = 0;

  mostrarFormularioAgregarCama: boolean = false;
  mostrarFormularioActualizarCama: boolean = false;
  mensajeExito: string | null = null;
  mensajeError: string | null = null;

  filtroUbicacion: string = '';
  filtroEstado: string = '';
  filtroTipo: string = '';

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.obtenerCamas();
  }

  obtenerCamas(): void {
    this.apiService.getCamas().subscribe({
      next: (data: Cama[]) => {
        this.camas = data;
        this.camasFiltradas = [...this.camas];
        this.totalPaginas = Math.ceil(this.camasFiltradas.length / this.camasPorPagina);
        this.verificarPaginaActual();
      },
      error: (error: any) => {
        console.error('Error al obtener las camas', error);
      }
    });
  }

  aplicarFiltros(): void {
    this.camasFiltradas = this.camas.filter(cama => {
      const coincideUbicacion = this.filtroUbicacion
        ? cama.Ubicacion.toLowerCase().includes(this.filtroUbicacion.toLowerCase())
        : true;
      const coincideEstado = this.filtroEstado ? cama.Estado === this.filtroEstado : true;
      const coincideTipo = this.filtroTipo ? cama.Tipo === this.filtroTipo : true;

      return coincideUbicacion && coincideEstado && coincideTipo;
    });

    this.totalPaginas = Math.ceil(this.camasFiltradas.length / this.camasPorPagina);
    this.verificarPaginaActual();
  }

  verificarPaginaActual(): void {
    if (this.paginaActual > this.totalPaginas) {
      this.paginaActual = this.totalPaginas; // Redirige a la última página si la actual es mayor
    }
    if (this.paginaActual < 1) {
      this.paginaActual = 1; // Redirige a la primera página si la actual es menor
    }
  }

  filtrarPorUbicacion(event: Event): void {
    const inputElement = event.target as HTMLInputElement;
    this.filtroUbicacion = inputElement.value;
    this.aplicarFiltros();
  }

  filtrarPorEstado(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    this.filtroEstado = selectElement.value;
    this.aplicarFiltros();
  }

  filtrarPorTipo(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    this.filtroTipo = selectElement.value;
    this.aplicarFiltros();
  }

  siguientePagina(): void {
    this.paginaActual++;
    this.verificarPaginaActual();
  }

  paginaAnterior(): void {
    this.paginaActual--;
    this.verificarPaginaActual();
  }

  primeraPagina(): void {
    this.paginaActual = 1;
  }

  ultimaPagina(): void {
    this.paginaActual = this.totalPaginas;
  }

  // Método para obtener las camas para la página actual
  obtenerCamasParaPagina(): Cama[] {
    const inicio = (this.paginaActual - 1) * this.camasPorPagina;
    return this.camasFiltradas.slice(inicio, inicio + this.camasPorPagina);
  }
}
