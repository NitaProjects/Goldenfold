import { Component } from '@angular/core';

@Component({
  selector: 'app-administrativo-dashboard',
  templateUrl: './administrativo-dashboard.component.html',
  styleUrls: ['./administrativo-dashboard.component.css']
})
export class AdministrativoDashboardComponent {
  patients = [
    { nombre: 'Juan Garcia', edad: 45, sintomas: 'Fiebre, Tos', estado: 'Estable' },
    { nombre: 'Maria Rodriguez', edad: 30, sintomas: 'Dolor de Cabeza', estado: 'Estable' }
  ];

  newPatient: any = {};

  logout() {
    console.log('Cerrar sesión');
  }

  registerPatient() {
    console.log('Registrar nuevo paciente:', this.newPatient);
    // Aquí puedes agregar la lógica para registrar al nuevo paciente.
  }
}
