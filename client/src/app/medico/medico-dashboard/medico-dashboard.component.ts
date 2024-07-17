
import { Component } from '@angular/core';

@Component({
  selector: 'app-medico-dashboard',
  templateUrl: './medico-dashboard.component.html',
  styleUrls: ['./medico-dashboard.component.css']
})
export class MedicoDashboardComponent {
  patients = [
    { nombre: 'Juan Garcia', edad: 45, sintomas: 'Fiebre, Tos', estado: 'En espera' },
    { nombre: 'Maria Rodriguez', edad: 30, sintomas: 'Dolor de Cabeza', estado: 'En espera' }
  ];

  availableBeds = [
    { ubicacion: 'Piso 1', estado: 'Disponible', tipo: 'UCI' },
    { ubicacion: 'Piso 2', estado: 'Ocupada', tipo: 'General' }
  ];

  selectedPatient: any = null;

  logout() {
    console.log('Cerrar sesión');
  }

  evaluatePatient(patient: any) {
    this.selectedPatient = patient;
  }

  submitEvaluation() {
    console.log('Evaluación enviada:', this.selectedPatient);
    // Aquí puedes agregar la lógica para manejar la evaluación del paciente.
  }
}
