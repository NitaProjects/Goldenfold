import { Component, OnInit } from '@angular/core';

interface Patient {
  name: string;
  age: number;
  symptoms: string;
  status: string;
}

@Component({
  selector: 'app-pacientes',
  templateUrl: './pacientes.component.html',
  styleUrls: ['./pacientes.component.css']
})
export class PacientesComponent implements OnInit {
  patients: Patient[] = [
    { name: 'Juan Garcia', age: 45, symptoms: 'Fiebre, Tos', status: 'En espera' },
    { name: 'Maria Rodriguez', age: 30, symptoms: 'Dolor de Cabeza', status: 'En espera' }
  ];

  constructor() { }

  ngOnInit(): void {
  }

  viewPatient(patient: Patient): void {
    // Lógica para ver detalles del paciente
  }

  editPatient(patient: Patient): void {
    // Lógica para editar información del paciente
  }

  requestBed(patient: Patient): void {
    // Lógica para solicitar una cama para el paciente
  }
}
