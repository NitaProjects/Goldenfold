import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pacientes-ingresados',
  templateUrl: './pacientes-ingresados.component.html',
  styleUrls: ['./pacientes-ingresados.component.css']
})
export class PacientesIngresadosComponent implements OnInit {
  pacientesIngresados = [
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
