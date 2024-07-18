import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pacientes-registrados',
  templateUrl: './pacientes-registrados.component.html',
  styleUrls: ['./pacientes-registrados.component.css']
})
export class PacientesRegistradosComponent implements OnInit {
  pacientes = [
    {
      idPaciente: 1, nombre: 'María López', edad: 30, sintomas: 'Fiebre y dolor de garganta', estado: 'En cama', 
      fechaRegistro: new Date('2024-07-15T09:46:33'), seguridadSocial: '123-45-6781', direccion: 'Avenida Siempre Viva 742', 
      telefono: '555-0001', email: 'maria.lopez@example.com', historialMedico: 'Historial médico de María', fechaNacimiento: new Date('1993-05-12')
    },
    {
      idPaciente: 2, nombre: 'Pedro García', edad: 40, sintomas: 'Dolor de pecho', estado: 'En cama', 
      fechaRegistro: new Date('2024-07-15T09:46:33'), seguridadSocial: '123-45-6782', direccion: 'Calle Falsa 123', 
      telefono: '555-0002', email: 'pedro.garcia@example.com', historialMedico: 'Historial médico de Pedro', fechaNacimiento: new Date('1983-07-24')
    },
    {
      idPaciente: 3, nombre: 'Laura Martínez', edad: 25, sintomas: 'Fractura de pierna', estado: 'En cama', 
      fechaRegistro: new Date('2024-07-15T09:46:33'), seguridadSocial: '123-45-6783', direccion: 'Avenida Principal 456', 
      telefono: '555-0003', email: 'laura.martinez@example.com', historialMedico: 'Historial médico de Laura', fechaNacimiento: new Date('1996-09-18')
    },
    {
      idPaciente: 4, nombre: 'Jorge Hernández', edad: 50, sintomas: 'Hipertensión', estado: 'En cama', 
      fechaRegistro: new Date('2024-07-15T09:46:33'), seguridadSocial: '123-45-6784', direccion: 'Calle Secundaria 789', 
      telefono: '555-0004', email: 'jorge.hernandez@example.com', historialMedico: 'Historial médico de Jorge', fechaNacimiento: new Date('1973-01-30')
    },
    {
      idPaciente: 5, nombre: 'Ana Rodríguez', edad: 35, sintomas: 'Dolor de cabeza crónico', estado: 'En cama', 
      fechaRegistro: new Date('2024-07-15T09:46:33'), seguridadSocial: '123-45-6785', direccion: 'Avenida Central 321', 
      telefono: '555-0005', email: 'ana.rodriguez@example.com', historialMedico: 'Historial médico de Ana', fechaNacimiento: new Date('1988-11-22')
    },
    {
      idPaciente: 6, nombre: 'Luis Gómez', edad: 45, sintomas: 'Problemas respiratorios', estado: 'Pendiente de cama', 
      fechaRegistro: new Date('2024-07-15T09:46:33'), seguridadSocial: '123-45-6786', direccion: 'Calle Norte 654', 
      telefono: '555-0006', email: 'luis.gomez@example.com', historialMedico: 'Historial médico de Luis', fechaNacimiento: new Date('1978-04-14')
    },
    {
      idPaciente: 7, nombre: 'Elena Fernández', edad: 28, sintomas: 'Infección urinaria', estado: 'Pendiente de cama', 
      fechaRegistro: new Date('2024-07-15T09:46:33'), seguridadSocial: '123-45-6787', direccion: 'Avenida Oeste 987', 
      telefono: '555-0007', email: 'elena.fernandez@example.com', historialMedico: 'Historial médico de Elena', fechaNacimiento: new Date('1995-08-07')
    },
    {
      idPaciente: 8, nombre: 'Carlos Sánchez', edad: 38, sintomas: 'Diarrea y deshidratación', estado: 'Pendiente de cama', 
      fechaRegistro: new Date('2024-07-15T09:46:33'), seguridadSocial: '123-45-6788', direccion: 'Calle Sur 321', 
      telefono: '555-0008', email: 'carlos.sanchez@example.com', historialMedico: 'Historial médico de Carlos', fechaNacimiento: new Date('1985-02-16')
    },
    {
      idPaciente: 9, nombre: 'Rosa Jiménez', edad: 60, sintomas: 'Dolor de espalda', estado: 'Pendiente de cama', 
      fechaRegistro: new Date('2024-07-15T09:46:33'), seguridadSocial: '123-45-6789', direccion: 'Avenida Este 654', 
      telefono: '555-0009', email: 'rosa.jimenez@example.com', historialMedico: 'Historial médico de Rosa', fechaNacimiento: new Date('1963-12-10')
    },
    {
      idPaciente: 10, nombre: 'Miguel Torres', edad: 22, sintomas: 'Alergia severa', estado: 'Pendiente de cama', 
      fechaRegistro: new Date('2024-07-15T09:46:33'), seguridadSocial: '123-45-6790', direccion: 'Calle Principal 987', 
      telefono: '555-0010', email: 'miguel.torres@example.com', historialMedico: 'Historial médico de Miguel', fechaNacimiento: new Date('2001-03-27')
    }
  ];

  constructor() { }

  ngOnInit(): void {
  }

  editRecord(id: number) {
    console.log(`Edit record with ID ${id}`);
  }

  copyRecord(id: number) {
    console.log(`Copy record with ID ${id}`);
  }

  deleteRecord(id: number) {
    console.log(`Delete record with ID ${id}`);
  }
}
