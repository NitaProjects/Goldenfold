import { Component, AfterViewInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { Router } from '@angular/router';



@Component({
  selector: 'app-administrativo-dashboard',
  templateUrl: './administrativo-dashboard.component.html',
  styleUrls: ['./administrativo-dashboard.component.css']
})
export class AdministrativoDashboardComponent implements AfterViewInit {

  constructor(private router: Router) {
    // Registrar todos los componentes necesarios de Chart.js
    Chart.register(...registerables);
  }

  ngAfterViewInit(): void {
    this.initializeCharts();
  }

  logout() {
    alert('Sesión cerrada');
    this.router.navigate(["/inicio"])
  }

  toggleSection(sectionId: string) {
    const section = document.getElementById(sectionId);
    if (section) {
      section.style.display = (section.style.display === 'none' || section.style.display === '') ? 'block' : 'none';
    }
  }

  registerPatient(event: Event) {
    event.preventDefault();
    alert('Paciente registrado');
  }


  initializeCharts() {
    // Gráfico de Distribución de Pacientes por Estado de Salud
    const ctx1 = document.getElementById('patientHealthStatusChart') as HTMLCanvasElement;
    if (ctx1) {
      new Chart(ctx1, {
        type: 'pie',
        data: {
          labels: ['Crítico', 'Estable', 'En Recuperación', 'Dados de Alta'],
          datasets: [{
            data: [5, 20, 10, 15], // Datos de ejemplo
            backgroundColor: [
              'rgba(255, 99, 132, 0.2)',
              'rgba(54, 162, 235, 0.2)',
              'rgba(255, 206, 86, 0.2)',
              'rgba(75, 192, 192, 0.2)'
            ],
            borderColor: [
              'rgba(255, 99, 132, 1)',
              'rgba(54, 162, 235, 1)',
              'rgba(255, 206, 86, 1)',
              'rgba(75, 192, 192, 1)'
            ],
            borderWidth: 1
          }]
        },
        options: {
          responsive: true
        }
      });
    }

    // Gráfico de Admisiones y Altas Diarias
    const ctx2 = document.getElementById('dailyAdmissionsDischargesChart') as HTMLCanvasElement;
    if (ctx2) {
      new Chart(ctx2, {
        type: 'line',
        data: {
          labels: ['01 Jul', '02 Jul', '03 Jul', '04 Jul', '05 Jul', '06 Jul', '07 Jul'], // Fechas de ejemplo
          datasets: [{
            label: 'Admisiones Diarias',
            data: [5, 10, 3, 7, 2, 8, 5], // Datos de ejemplo
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderColor: 'rgba(75, 192, 192, 1)',
            borderWidth: 1,
            fill: true
          }, {
            label: 'Altas Diarias',
            data: [3, 5, 2, 4, 6, 1, 7], // Datos de ejemplo
            backgroundColor: 'rgba(255, 99, 132, 0.2)',
            borderColor: 'rgba(255, 99, 132, 1)',
            borderWidth: 1,
            fill: true
          }]
        },
        options: {
          responsive: true,
          scales: {
            y: {
              beginAtZero: true
            }
          }
        }
      });
    }
  }
}
