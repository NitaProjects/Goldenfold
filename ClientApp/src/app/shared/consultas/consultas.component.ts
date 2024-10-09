import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService, Consulta } from '../../services/api.service';

@Component({
  selector: 'app-consultas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './consultas.component.html',
  styleUrls: ['./consultas.component.css']
})
export class ConsultasComponent implements OnInit {
  consultas: Consulta[] = [];
  nuevaConsulta: Consulta = {
    IdConsulta: 0,
    IdPaciente: 0,
    IdMedico: 0,
    Motivo: '',
    FechaSolicitud: new Date(),
    FechaConsulta: null,
    Estado: ''
  };
  consultaParaActualizar: Consulta | null = null;

  constructor(private apiService: ApiService) {}

  ngOnInit(): void {
    this.obtenerConsultas(); 
  }

  obtenerConsultas(): void {
    this.apiService.getConsultas().subscribe({
      next: (data: Consulta[]) => {
        this.consultas = data;
      },
      error: (error: any) => {
        console.error('Error al obtener las consultas', error);
      }
    });
  }

  agregarConsulta(): void {
    this.apiService.addConsulta(this.nuevaConsulta).subscribe({
      next: (nuevaConsulta: Consulta) => {
        this.consultas.push(nuevaConsulta);
        this.nuevaConsulta = {
          IdConsulta: 0,
          IdPaciente: 0,
          IdMedico: 0,
          Motivo: '',
          FechaSolicitud: new Date(),
          FechaConsulta: null,
          Estado: ''
        };
      },
      error: (error: any) => {
        console.error('Error al agregar la consulta', error);
      }
    });
  }

  actualizarConsulta(): void {
    if (this.consultaParaActualizar) {
      this.apiService.updateConsulta(this.consultaParaActualizar).subscribe({
        next: (consultaActualizada: Consulta) => {
          const index = this.consultas.findIndex(c => c.IdConsulta === consultaActualizada.IdConsulta);
          if (index !== -1) {
            this.consultas[index] = consultaActualizada;
          }
          this.consultaParaActualizar = null;
        },
        error: (error: any) => {
          console.error('Error al actualizar la consulta', error);
        }
      });
    }
  }

  borrarConsulta(id: number): void {
    this.apiService.deleteConsulta(id).subscribe({
      next: () => {
        this.consultas = this.consultas.filter(c => c.IdConsulta !== id);
      },
      error: (error: any) => {
        console.error('Error al borrar la consulta', error);
      }
    });
  }

  toggleActualizarConsulta(consulta: Consulta): void {
    this.consultaParaActualizar = consulta;
  }
}
