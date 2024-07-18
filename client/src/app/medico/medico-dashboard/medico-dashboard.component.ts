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

  updatePatient(event: Event, patientId: number) {
    event.preventDefault();
    alert(`Paciente ${patientId} actualizado`);
  }

  submitAssignment(event: Event, patientId: number) {
    event.preventDefault();
    alert(`Cama asignada al paciente ${patientId}`);
  }

  initializeCharts() {
    const ctx1 = document.getElementById('patientAgeChart') as HTMLCanvasElement;
    if (ctx1) {
      new Chart(ctx1, {
        type: 'bar',
        data: {
          labels: ['0-10', '11-20', '21-30', '31-40', '41-50', '51-60', '61-70', '71-80', '81-90', '90+'],
          datasets: [{
            label: 'Número de Pacientes',
            data: [2, 3, 1, 5, 4, 6, 2, 1, 1, 0],
            backgroundColor: 'rgba(54, 162, 235, 0.2)',
            borderColor: 'rgba(54, 162, 235, 1)',
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

    const ctx2 = document.getElementById('bedStatusChart') as HTMLCanvasElement;
    if (ctx2) {
      new Chart(ctx2, {
        type: 'pie',
        data: {
          labels: ['Disponible', 'Ocupada', 'Mantenimiento'],
          datasets: [{
            label: 'Estado de las Camas',
            data: [10, 5, 1],
            backgroundColor: [
              'rgba(75, 192, 192, 0.2)',
              'rgba(255, 99, 132, 0.2)',
              'rgba(255, 206, 86, 0.2)'
            ],
            borderColor: [
              'rgba(75, 192, 192, 1)',
              'rgba(255, 99, 132, 1)',
              'rgba(255, 206, 86, 1)'
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
