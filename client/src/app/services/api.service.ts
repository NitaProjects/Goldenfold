// src/app/services/api.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Paciente {
  iD_Paciente: number;
  nombre: string;
  edad: number;
  sintomas: string;
  estado: string;
  fecha_Registro: Date;
  seguridad_Social: string;
  direccion: string;
  telefono: string;
  historial_Medico: string;
  fecha_Nacimiento: Date;
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'http://localhost:5076/api';

  constructor(private http: HttpClient) { }

  getPacientes(): Observable<Paciente[]> {
    return this.http.get<Paciente[]>(`${this.apiUrl}/Pacientes`);
  }

  deletePaciente(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Pacientes/${id}`);
  }
}
