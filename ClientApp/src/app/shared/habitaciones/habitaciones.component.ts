import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService, Habitacion } from '../../services/api.service';
import { SnackbarComponent } from '../snackbar/snackbar.component'; // Importar el componente standalone
import { MatTableModule, MatTableDataSource } from '@angular/material/table'; // Módulo de tabla de Angular Material
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator'; // Módulo de paginación de Angular Material
import { MatSortModule, MatSort } from '@angular/material/sort'; // Módulo de ordenación de Angular Material
import { MatFormFieldModule } from '@angular/material/form-field'; // Para los campos de formulario
import { MatInputModule } from '@angular/material/input'; // Para los campos de entrada
import { MatButtonModule } from '@angular/material/button'; // Para los botones
import { MatSelectModule } from '@angular/material/select'; // Para los selectores
import { MatCardModule } from '@angular/material/card'; // Para las tarjetas

@Component({
  selector: 'app-habitaciones',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    SnackbarComponent,
    MatTableModule, // Importar MatTableModule para las tablas
    MatPaginatorModule, // Importar MatPaginatorModule para la paginación
    MatSortModule, // Importar MatSortModule para la ordenación
    MatFormFieldModule, // Para el campo de formulario de Angular Material
    MatInputModule, // Para los inputs de Angular Material
    MatButtonModule, // Para los botones de Angular Material
    MatSelectModule, // Para los selectores de Angular Material
    MatCardModule // Para las tarjetas
  ],
  templateUrl: './habitaciones.component.html',
  styleUrls: ['./habitaciones.component.css'],
})
export class HabitacionesComponent implements OnInit {
  @ViewChild(SnackbarComponent) snackbar!: SnackbarComponent;  // Referencia al snackbar
  @ViewChild(MatPaginator) paginator!: MatPaginator;  // Referencia al paginador de Angular Material
  @ViewChild(MatSort) sort!: MatSort;  // Referencia al orden de Angular Material

  habitaciones: Habitacion[] = [];
  habitacionesDataSource = new MatTableDataSource<Habitacion>();  // DataSource para la tabla
  nuevaHabitacion: Habitacion = {
    IdHabitacion: 0,
    Edificio: '',
    Planta: '',
    NumeroHabitacion: '',
    TipoCama: '',
  };
  habitacionParaActualizar: Habitacion | null = null;

  displayedColumns: string[] = ['Edificio', 'Planta', 'NumeroHabitacion', 'Acciones'];  // Columnas de la tabla

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.obtenerHabitaciones();
  }

  ngAfterViewInit() {
    this.habitacionesDataSource.paginator = this.paginator;  // Enlazar el paginador a la tabla
    this.habitacionesDataSource.sort = this.sort;  // Enlazar la ordenación a la tabla
  }

  obtenerHabitaciones(): void {
    this.apiService.getHabitaciones().subscribe({
      next: (data: Habitacion[]) => {
        this.habitaciones = data;
        this.habitacionesDataSource.data = this.habitaciones;  // Asignar los datos al datasource de la tabla
      },
      error: (error: any) => {
        console.error('Error al obtener las habitaciones', error);
        this.snackbar.showNotification('error', 'Error al obtener las habitaciones');  // Mostrar notificación de error
      },
    });
  }

  agregarHabitacion(): void {
    if (this.nuevaHabitacion.Edificio && this.nuevaHabitacion.Planta && this.nuevaHabitacion.NumeroHabitacion && this.nuevaHabitacion.TipoCama) {
      this.apiService.addHabitacion(this.nuevaHabitacion).subscribe({
        next: (nuevaHabitacion: Habitacion) => {
          this.habitaciones.push(nuevaHabitacion);
          this.habitacionesDataSource.data = this.habitaciones;  // Actualizar los datos en la tabla
          this.nuevaHabitacion = { IdHabitacion: 0, Edificio: '', Planta: '', NumeroHabitacion: '', TipoCama: '' };  // Resetear el formulario
          this.snackbar.showNotification('success', 'Habitación agregada exitosamente');  // Mostrar notificación de éxito
        },
        error: (error: any) => {
          console.error('Error al agregar la habitación', error);
          this.snackbar.showNotification('error', 'Error al agregar la habitación');  // Mostrar notificación de error
        },
      });
    } else {
      this.snackbar.showNotification('error', 'Por favor, completa todos los campos');  // Notificación para campos faltantes
    }
  }

  actualizarHabitacion(): void {
    if (this.habitacionParaActualizar) {
      this.apiService.updateHabitacion(this.habitacionParaActualizar).subscribe({
        next: (habitacionActualizada: Habitacion) => {
          const index = this.habitaciones.findIndex(
            (h) => h.IdHabitacion === habitacionActualizada.IdHabitacion
          );
          if (index !== -1) {
            this.habitaciones[index] = habitacionActualizada;
            this.habitacionesDataSource.data = this.habitaciones;  // Actualizar la tabla con los datos actualizados
          }
          this.habitacionParaActualizar = null;  // Limpiar el formulario de actualización
          this.snackbar.showNotification('success', 'Habitación actualizada correctamente');  // Mostrar notificación de éxito
        },
        error: (error: any) => {
          console.error('Error al actualizar la habitación', error);
          this.snackbar.showNotification('error', 'Error al actualizar la habitación');  // Mostrar notificación de error
        },
      });
    }
  }

  borrarHabitacion(id: number): void {
    const confirmacion = confirm('¿Estás seguro de que deseas eliminar esta habitación?');
    if (confirmacion) {
      this.apiService.deleteHabitacion(id).subscribe({
        next: () => {
          this.habitaciones = this.habitaciones.filter(h => h.IdHabitacion !== id);
          this.habitacionesDataSource.data = this.habitaciones;  // Actualizar la tabla sin la habitación borrada
          this.snackbar.showNotification('success', 'Habitación borrada correctamente');  // Mostrar notificación de éxito
        },
        error: (error: any) => {
          console.error('Error al borrar la habitación', error);
          this.snackbar.showNotification('error', 'Error al borrar la habitación');  // Mostrar notificación de error
        },
      });
    }
  }

  toggleActualizarHabitacion(habitacion: Habitacion): void {
    this.habitacionParaActualizar = habitacion;  // Mostrar los datos en el formulario de actualización
  }

  aplicarFiltro(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.habitacionesDataSource.filter = filterValue.trim().toLowerCase();

    if (this.habitacionesDataSource.paginator) {
      this.habitacionesDataSource.paginator.firstPage();  // Reiniciar la paginación cuando se aplica el filtro
    }
  }

  cancelarNuevoHabitacion(): void {
    // Resetear el formulario de agregar habitación
    this.nuevaHabitacion = {
      IdHabitacion: 0,
      Edificio: '',
      Planta: '',
      NumeroHabitacion: '',
      TipoCama: '',
    };
  }
  
}
