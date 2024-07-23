// src/app/services/api.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// Definición de la interfaz Paciente
export interface Paciente {
  IdPaciente: number;
  Nombre: string;
  Edad: number;
  FechaNacimiento: Date;
  Sintomas: string;
  Estado: string;
  FechaRegistro: Date;
  SeguridadSocial: string;
  Direccion: string;
  Telefono: string;
  HistorialMedico: string;
}

// Definición de la interfaz HistorialAlta
export interface HistorialAlta {
  IdHistorial: number;
  IdPaciente: number;
  FechaAlta: Date;
  Diagnostico: string;
  Tratamiento: string;
}

// Definición de la interfaz Rol
export interface Rol {
  IdRol: number;
  NombreRol: string;
}

// Definición de la interfaz Asignacion
export interface Asignacion {
  IdAsignacion: number;
  IdPaciente: number;
  Ubicacion: string;
  FechaAsignacion: Date;
  FechaLiberacion: Date;
  AsignadoPor: number;
}

// Definición de la interfaz Habitación
export interface Habitacion {
  IdHabitacion: number;
  Edificio: string;
  Planta: string;
  NumeroHabitacion: string;
}

// Definición de la interfaz Cama
export interface Cama {
  Ubicacion: string;
  Estado: string;
  Tipo: string;
}

// Definición de la interfaz Usuario
export interface Usuario {
  IdUsuario: number;
  Nombre: string;
  NombreUsuario: string;
  Contrasenya: string;
  IdRol: number;
}

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = 'http://localhost:5076/api'; // URL base de la API

  constructor(private http: HttpClient) { }

  // CRUD para Pacientes
  getPacientes(): Observable<Paciente[]> {
    return this.http.get<Paciente[]>(`${this.apiUrl}/Pacientes`);
  }

  getPacienteById(id: number): Observable<Paciente> {
    return this.http.get<Paciente>(`${this.apiUrl}/Pacientes/${id}`);
  }

  addPaciente(paciente: Paciente): Observable<Paciente> {
    return this.http.post<Paciente>(`${this.apiUrl}/Pacientes`, paciente);
  }

  updatePaciente(paciente: Paciente): Observable<Paciente> {
    return this.http.put<Paciente>(`${this.apiUrl}/Pacientes/${paciente.IdPaciente}`, paciente);
  }

  deletePaciente(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Pacientes/${id}`);
  }

  // CRUD para HistorialAltas
  getHistorialAltas(): Observable<HistorialAlta[]> {
    return this.http.get<HistorialAlta[]>(`${this.apiUrl}/HistorialAltas`);
  }

  getHistorialAltaById(id: number): Observable<HistorialAlta> {
    return this.http.get<HistorialAlta>(`${this.apiUrl}/HistorialAltas/${id}`);
  }

  addHistorialAlta(historialAlta: HistorialAlta): Observable<HistorialAlta> {
    return this.http.post<HistorialAlta>(`${this.apiUrl}/HistorialAltas`, historialAlta);
  }

  updateHistorialAlta(historialAlta: HistorialAlta): Observable<HistorialAlta> {
    return this.http.put<HistorialAlta>(`${this.apiUrl}/HistorialAltas/${historialAlta.IdHistorial}`, historialAlta);
  }

  deleteHistorialAlta(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/HistorialAltas/${id}`);
  }

  // CRUD para Roles
  getRoles(): Observable<Rol[]> {
    return this.http.get<Rol[]>(`${this.apiUrl}/Roles`);
  }

  getRolById(id: number): Observable<Rol> {
    return this.http.get<Rol>(`${this.apiUrl}/Roles/${id}`);
  }

  addRol(rol: Rol): Observable<Rol> {
    return this.http.post<Rol>(`${this.apiUrl}/Roles`, rol);
  }

  updateRol(rol: Rol): Observable<Rol> {
    return this.http.put<Rol>(`${this.apiUrl}/Roles/${rol.IdRol}`, rol);
  }

  deleteRol(idRol: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Roles/${idRol}`);
  }

  // CRUD para Asignaciones
  getAsignaciones(): Observable<Asignacion[]> {
    return this.http.get<Asignacion[]>(`${this.apiUrl}/Asignaciones`);
  }

  getAsignacionById(id: number): Observable<Asignacion> {
    return this.http.get<Asignacion>(`${this.apiUrl}/Asignaciones/${id}`);
  }

  addAsignacion(asignacion: Asignacion): Observable<Asignacion> {
    return this.http.post<Asignacion>(`${this.apiUrl}/Asignaciones`, asignacion);
  }

  updateAsignacion(asignacion: Asignacion): Observable<Asignacion> {
    return this.http.put<Asignacion>(`${this.apiUrl}/Asignaciones/${asignacion.IdAsignacion}`, asignacion);
  }

  deleteAsignacion(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Asignaciones/${id}`);
  }

  // CRUD para Usuarios
  getUsuarios(): Observable<Usuario[]> {
    return this.http.get<Usuario[]>(`${this.apiUrl}/Usuarios`);
  }

  getUsuarioById(id: number): Observable<Usuario> {
    return this.http.get<Usuario>(`${this.apiUrl}/Usuarios/${id}`);
  }

  addUsuario(Usuario: Usuario): Observable<Usuario> {
    return this.http.post<Usuario>(`${this.apiUrl}/Usuarios`, Usuario);
  }

  updateUsuario(Usuario: Usuario): Observable<Usuario> {
    return this.http.put<Usuario>(`${this.apiUrl}/Usuarios/${Usuario.IdUsuario}`, Usuario);
  }

  deleteUsuario(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Usuarios/${id}`);
  }

  // CRUD para Camas
  getCamas(): Observable<Cama[]> {
    return this.http.get<Cama[]>(`${this.apiUrl}/Camas`);
  }

  getCamaByUbi(Ubicacion: string): Observable<Cama> {
    return this.http.get<Cama>(`${this.apiUrl}/Camas/${Ubicacion}`);
  }

  addCama(Cama: Cama): Observable<Cama> {
    return this.http.post<Cama>(`${this.apiUrl}/Camas`, Cama);
  }

  updateCama(Cama: Cama): Observable<Cama> {
    return this.http.put<Cama>(`${this.apiUrl}/Camas/${Cama.Ubicacion}`, Cama);
  }

  deleteCama(Ubicacion: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Camas/${Ubicacion}`);
  }

  // CRUD para Habitaciones
  getHabitaciones(): Observable<Habitacion[]> {
    return this.http.get<Habitacion[]>(`${this.apiUrl}/Habitaciones`);
  }

  getHabitacionById(id: number): Observable<Habitacion> {
    return this.http.get<Habitacion>(`${this.apiUrl}/Habitaciones/${id}`);
  }

  addHabitacion(Habitacion: Habitacion): Observable<Habitacion> {
    return this.http.post<Habitacion>(`${this.apiUrl}/Habitaciones`, Habitacion);
  }

  updateHabitacion(Habitacion: Habitacion): Observable<Habitacion> {
    return this.http.put<Habitacion>(`${this.apiUrl}/Habitaciones/${Habitacion.IdHabitacion}`, Habitacion);
  }

  deleteHabitacion(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Habitaciones/${id}`);
  }
}
