import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpParams,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';

// Definición de las interfaces de las tablas
export interface Paciente {
  IdPaciente: number;
  Nombre: string;
  Dni: string;
  FechaNacimiento: Date;
  Estado: string;
  FechaRegistro: Date;
  SeguridadSocial: string;
  Direccion: string;
  Telefono: string;
  Email: string;
  HistorialMedico: string;
}

export interface Consulta {
  IdConsulta: number;
  IdPaciente: number;
  IdMedico: number;
  Motivo: string;
  FechaSolicitud: Date;
  FechaConsulta: Date | null;
  Estado: string;
}

export interface Ingreso {
  IdIngreso: number;
  IdPaciente: number;
  IdMedico: number;
  Motivo: string;
  FechaSolicitud: Date;
  FechaIngreso: Date | null;
  Estado: string;
  TipoCama: string;
  IdAsignacion: number | null;
}

export interface HistorialAlta {
  IdHistorial: number;
  IdPaciente: number;
  IdMedico: number;
  FechaAlta: Date;
  Diagnostico: string;
  Tratamiento: string;
}

export interface Asignacion {
  IdAsignacion: number;
  IdPaciente: number;
  IdCama: number;
  FechaAsignacion: Date;
  FechaLiberacion: Date | null;
  AsignadoPor: number;
}

export interface Usuario {
  IdUsuario: number;
  Nombre: string;
  NombreUsuario: string;
  Contrasenya: string;
  IdRol: number;
}

export interface Cama {
  IdCama: number;
  Ubicacion: string;
  Estado: string;
  Tipo: string;
  IdHabitacion: number;
}

export interface Habitacion {
  IdHabitacion: number;
  Edificio: string;
  Planta: string;
  NumeroHabitacion: string;
  TipoCama: string;
}

export interface Rol {
  IdRol: number;
  NombreRol: string;
}

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private apiUrl = 'http://localhost:5076/api';

  constructor(private http: HttpClient) { }

 // CRUD para Pacientes
  getPacientes(Nombre?: string, numSS?: string): Observable<Paciente[]> {
    let params = new HttpParams();
    if (Nombre) params = params.set('nombre', Nombre);
    if (numSS) params = params.set('numSS', numSS);
    return this.http.get<Paciente[]>(`${this.apiUrl}/Pacientes`, { params });
  }

  getPacienteById(id: number): Observable<any> {
    return this.http.get(`${this.apiUrl}/Pacientes/${id}`);
  }
  addPaciente(paciente: Paciente): Observable<Paciente> {
    return this.http.post<Paciente>(`${this.apiUrl}/Pacientes`, paciente);
  }

  updatePaciente(paciente: Paciente): Observable<Paciente> {
    return this.http.put<Paciente>(
      `${this.apiUrl}/Pacientes/${paciente.IdPaciente}`,
      paciente
    );
  }

  deletePaciente(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Pacientes/${id}`);
  }

  // CRUD para Consultas
  getConsultas(
    idPaciente?: number,
    idMedico?: number,
    estado?: string,
    fechaSolicitud?: Date,
    fechaConsulta?: Date,
    motivo?: string
  ): Observable<Consulta[]> {
    let params = new HttpParams();
    if (idPaciente) params = params.set('idPaciente', idPaciente.toString());
    if (idMedico) params = params.set('idMedico', idMedico.toString());
    if (estado) params = params.set('estado', estado);
    if (fechaSolicitud)
      params = params.set('fechaSolicitud', fechaSolicitud.toISOString());
    if (fechaConsulta)
      params = params.set('fechaConsulta', fechaConsulta.toISOString());
    if (motivo) params = params.set('motivo', motivo);

    return this.http.get<Consulta[]>(`${this.apiUrl}/Consultas`, { params });
  }

  addConsulta(consulta: Consulta): Observable<Consulta> {
    return this.http.post<Consulta>(`${this.apiUrl}/Consultas`, consulta);
  }

  updateConsulta(consulta: Consulta): Observable<Consulta> {
    return this.http.put<Consulta>(
      `${this.apiUrl}/Consultas/${consulta.IdConsulta}`,
      consulta
    );
  }

  deleteConsulta(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Consultas/${id}`);
  }

  // CRUD para Ingresos
  getIngresos(
    idPaciente?: number,
    idMedico?: number,
    estado?: string,
    tipoCama?: string 
  ): Observable<Ingreso[]> {
    let params = new HttpParams();
    if (idPaciente) params = params.set('idPaciente', idPaciente.toString());
    if (idMedico) params = params.set('idMedico', idMedico.toString());
    if (estado) params = params.set('estado', estado);
    if (tipoCama) params = params.set('tipoCama', tipoCama); 
    return this.http.get<Ingreso[]>(`${this.apiUrl}/Ingresos`, { params });
  }

  addIngreso(ingreso: Ingreso): Observable<Ingreso> {
    return this.http.post<Ingreso>(`${this.apiUrl}/Ingresos`, ingreso);
  }

  updateIngreso(ingreso: Ingreso): Observable<Ingreso> {
    return this.http.put<Ingreso>(
      `${this.apiUrl}/Ingresos/${ingreso.IdIngreso}`,
      ingreso
    );
  }

  deleteIngreso(idIngreso: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Ingresos/${idIngreso}`);
  }


  getAsignaciones(
    idPaciente?: number,
    idCama?: number,
    fechaAsignacion?: Date,
    fechaLiberacion?: Date | null,
    asignadoPor?: number
  ): Observable<Asignacion[]> {
    let params = new HttpParams();

    if (idPaciente) params = params.set('idPaciente', idPaciente.toString());
    if (idCama) params = params.set('idCama', idCama.toString());
    if (fechaAsignacion) params = params.set('fechaAsignacion', fechaAsignacion.toISOString());
    if (fechaLiberacion) params = params.set('fechaLiberacion', fechaLiberacion.toISOString());
    if (asignadoPor) params = params.set('asignadoPor', asignadoPor.toString());

    return this.http.get<Asignacion[]>(`${this.apiUrl}/Asignaciones`, { params });
  }

  addAsignacion(asignacion: Asignacion): Observable<Asignacion> {
    return this.http.post<Asignacion>(
      `${this.apiUrl}/Asignaciones`,
      asignacion
    );
  }

  updateAsignacion(asignacion: Asignacion): Observable<Asignacion> {
    return this.http.put<Asignacion>(
      `${this.apiUrl}/Asignaciones/${asignacion.IdAsignacion}`,
      asignacion
    );
  }

  deleteAsignacion(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Asignaciones/${id}`);
  }

  // CRUD para HistorialAltas
  getHistorialAltas(
    idPaciente?: number,
    fechaAlta?: Date,
    diagnostico?: string,
    tratamiento?: string
  ): Observable<HistorialAlta[]> {
    let params = new HttpParams();
    if (idPaciente) params = params.set('idPaciente', idPaciente.toString());
    if (fechaAlta) params = params.set('fechaAlta', fechaAlta.toISOString());
    if (diagnostico) params = params.set('diagnostico', diagnostico);
    if (tratamiento) params = params.set('tratamiento', tratamiento);

    return this.http.get<HistorialAlta[]>(`${this.apiUrl}/HistorialAltas`, {
      params,
    });
  }

  addHistorialAlta(historialAlta: HistorialAlta): Observable<HistorialAlta> {
    return this.http.post<HistorialAlta>(
      `${this.apiUrl}/HistorialAltas`,
      historialAlta
    );
  }

  updateHistorialAlta(historialAlta: HistorialAlta): Observable<HistorialAlta> {
    return this.http.put<HistorialAlta>(
      `${this.apiUrl}/HistorialAltas/${historialAlta.IdHistorial}`,
      historialAlta
    );
  }

  deleteHistorialAlta(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/HistorialAltas/${id}`);
  }

  // CRUD para Usuarios
  getUsuarios(
    nombre?: string,
    nombreUsuario?: string,
    idRol?: number
  ): Observable<Usuario[]> {
    let params = new HttpParams();
    if (nombre) params = params.set('nombre', nombre);
    if (nombreUsuario) params = params.set('usuario', nombreUsuario);
    if (idRol) params = params.set('idRol', idRol.toString());
    return this.http.get<Usuario[]>(`${this.apiUrl}/Usuarios`, { params });
  }

  addUsuario(usuario: Usuario): Observable<Usuario> {
    return this.http.post<Usuario>(`${this.apiUrl}/Usuarios`, usuario);
  }

  updateUsuario(usuario: Usuario): Observable<Usuario> {
    return this.http.put<Usuario>(
      `${this.apiUrl}/Usuarios/${usuario.IdUsuario}`,
      usuario
    );
  }

  deleteUsuario(idUsuario: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Usuarios/${idUsuario}`);
  }

  // CRUD para Camas
  getCamas(
    Ubicacion?: string,
    Estado?: string,
    Tipo?: string,
    IdHabitacion?: number,
    IdCama?: number
  ): Observable<Cama[]> {
    let params = new HttpParams();
    if (Ubicacion) params = params.set('ubicacion', Ubicacion);
    if (Estado) params = params.set('estado', Estado);
    if (Tipo) params = params.set('tipo', Tipo);
    if (IdHabitacion)
      params = params.set('idHabitacion', IdHabitacion.toString());
    if (IdCama) params = params.set('idCama', IdCama.toString());
    return this.http.get<Cama[]>(`${this.apiUrl}/Camas`, { params });
  }

  // CRUD para Habitaciones
  getHabitaciones(
    Edificio?: string,
    Planta?: string,
    IdHabitacion?: number
  ): Observable<Habitacion[]> {
    let params = new HttpParams();
    if (Edificio) params = params.set('Edificio', Edificio);
    if (Planta) params = params.set('Planta', Planta);
    if (IdHabitacion)
      params = params.set('IdHabitacion', IdHabitacion.toString());

    return this.http.get<Habitacion[]>(`${this.apiUrl}/Habitaciones`, {
      params,
    });
  }

  addHabitacion(Habitacion: Habitacion): Observable<Habitacion> {
    return this.http.post<Habitacion>(
      `${this.apiUrl}/Habitaciones`,
      Habitacion
    );
  }

  updateHabitacion(Habitacion: Habitacion): Observable<Habitacion> {
    return this.http.put<Habitacion>(
      `${this.apiUrl}/Habitaciones/${Habitacion.IdHabitacion}`,
      Habitacion
    );
  }

  deleteHabitacion(IdHabitacion: number): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}/Habitaciones/${IdHabitacion}`
    );
  }

  // CRUD para Roles
  getRoles(NombreRol?: string): Observable<Rol[]> {
    let params = new HttpParams();
    if (NombreRol) {
      params = params.set('nombreRol', NombreRol);
    }
    return this.http.get<Rol[]>(`${this.apiUrl}/Roles`, { params });
  }

  getRolById(IdRol: number): Observable<Rol> {
    return this.http.get<Rol>(`${this.apiUrl}/Roles/${IdRol}`);
  }

  addRol(Rol: Rol): Observable<Rol> {
    return this.http.post<Rol>(`${this.apiUrl}/Roles`, Rol);
  }

  updateRol(Rol: Rol): Observable<Rol> {
    return this.http.put<Rol>(`${this.apiUrl}/Roles/${Rol.IdRol}`, Rol);
  }

  deleteRol(IdRol: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/Roles/${IdRol}`);
  }
}
