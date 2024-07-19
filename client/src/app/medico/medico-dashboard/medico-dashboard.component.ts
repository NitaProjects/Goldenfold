import { Component, AfterViewInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { Router } from '@angular/router';

@Component({
  selector: 'app-medico-dashboard',
  templateUrl: './medico-dashboard.component.html',
  styleUrls: ['./medico-dashboard.component.css']
})
export class MedicoDashboardComponent implements AfterViewInit {

  constructor(private router: Router) {
    // Registrar todos los componentes necesarios de Chart.js
    Chart.register(...registerables);
  }

  ngAfterViewInit(): void {
    this.initializeCharts();
  }

  logout() {
    alert('Sesión cerrada');
    this.router.navigate(["/inicio"]);
  }

  toggleSection(sectionId: string) {
    const section = document.getElementById(sectionId);
    if (section) {
      section.style.display = (section.style.display === 'none' || section.style.display === '') ? 'block' : 'none';
    }
  }

  evaluatePatient(event: Event, patientId: number) {
    event.preventDefault();
    const form = event.target as HTMLFormElement;
    const formData = new FormData(form);

    // Aquí enviarías los datos al backend
    console.log('Evaluando paciente:', patientId, formData);

    // Ejemplo: resetear formulario y ocultar sección
    form.reset();
    this.toggleSection(`view-evaluate-section-${patientId}`);
    alert(`Paciente ${patientId} evaluado para ingreso`);
  }

  initializeCharts() {
    // Gráfico de disponibilidad de camas por tipo
    const ctx1 = document.getElementById('bedAvailabilityChart') as HTMLCanvasElement;
    if (ctx1) {
      new Chart(ctx1, {
        type: 'bar',
        data: {
          labels: ['General', 'UCI', 'Postoperatorio'],
          datasets: [{
            label: 'Camas Disponibles',
            data: [20, 5, 10], // Datos de ejemplo
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderColor: 'rgba(75, 192, 192, 1)',
            borderWidth: 1
          }, {
            label: 'Camas Ocupadas',
            data: [10, 3, 5], // Datos de ejemplo
            backgroundColor: 'rgba(255, 99, 132, 0.2)',
            borderColor: 'rgba(255, 99, 132, 1)',
            borderWidth: 1
          }]
        },
        options: {
          scales: {
            y: {
              beginAtZero: true
            }
          }
        }
      });
    }

    // Gráfico de capacidad total del hospital
    const ctx2 = document.getElementById('totalCapacityChart') as HTMLCanvasElement;
    if (ctx2) {
      new Chart(ctx2, {
        type: 'doughnut',
        data: {
          labels: ['Capacidad Total', 'Capacidad Usada'],
          datasets: [{
            data: [100, 60], // Datos de ejemplo
            backgroundColor: [
              'rgba(54, 162, 235, 0.2)',
              'rgba(255, 159, 64, 0.2)'
            ],
            borderColor: [
              'rgba(54, 162, 235, 1)',
              'rgba(255, 159, 64, 1)'
            ],
            borderWidth: 1
          }]
        },
        options: {
          responsive: true
        }
      });
    }
  }
}
