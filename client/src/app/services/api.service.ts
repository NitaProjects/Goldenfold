import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// Definición de la interfaz Paciente
export interface Paciente {
  id_paciente: number;
  nombre: string;
  edad: number;
  sintomas: string;
  estado: string;
  fecha_registro: Date;
  seguridad_social: string;
  direccion: string;
  telefono: string;
  historial_medico: string;
  fecha_nacimiento: Date;
}

// Definición de la interfaz HistorialAlta
export interface HistorialAlta {
  id_historial: number;
  id_paciente: number;
  fecha_alta: Date;
  diagnostico: string;
  tratamiento: string;
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'http://localhost:5076/api'; // URL base de la API

  constructor(private http: HttpClient) { }

  // Método para obtener todos los pacientes
  getPacientes(): Observable<Paciente[]> {
    return this.http.get<Paciente[]>(`${this.apiUrl}/Pacientes`);
  }

  // Método para eliminar un paciente por ID
  deletePaciente(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Pacientes/${id}`);
  }

  // Método para obtener todos los registros de historial de altas
  getHistorialAltas(): Observable<HistorialAlta[]> {
    return this.http.get<HistorialAlta[]>(`${this.apiUrl}/HistorialAltas`);
  }

  // Método para agregar un nuevo registro de historial de alta
  addHistorialAlta(historialAlta: HistorialAlta): Observable<HistorialAlta> {
    return this.http.post<HistorialAlta>(`${this.apiUrl}/HistorialAltas`, historialAlta);
  }

  // Método para eliminar un registro de historial de alta por ID
  deleteHistorialAlta(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/HistorialAltas/${id}`);
  }

  // Método para actualizar un registro de historial de alta
  updateHistorialAlta(id: number, historialAlta: HistorialAlta): Observable<HistorialAlta> {
    return this.http.put<HistorialAlta>(`${this.apiUrl}/HistorialAltas/${id}`, historialAlta);
  }
}
