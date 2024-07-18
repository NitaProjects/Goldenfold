import { Component, AfterViewInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { Router } from '@angular/router';
  
  
@Component({
  selector: 'app-controlador-camas-dashboard',
  templateUrl: './controlador-camas-dashboard.component.html',
  styleUrls: ['./controlador-camas-dashboard.component.css']
})
export class ControladorCamasDashboardComponent implements AfterViewInit {

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

  filterCamas() {
    const tipoCama = (document.getElementById('filter-tipo-cama') as HTMLSelectElement).value;
    const estadoCama = (document.getElementById('filter-estado-cama') as HTMLSelectElement).value;

    const rows = document.querySelectorAll('#camas-table-body tr');
    rows.forEach(row => {
      const tableRow = row as HTMLTableRowElement;
      const tipo = tableRow.cells[1].innerText.toLowerCase();
      const estado = tableRow.cells[2].innerText.toLowerCase();

      let tipoMatch = tipoCama ? tipo === tipoCama : true;
      let estadoMatch = estadoCama ? estado === estadoCama : true;

      if (tipoMatch && estadoMatch) {
        tableRow.style.display = '';
      } else {
        tableRow.style.display = 'none';
      }
    });
  }

  assignBed(patientId: number) {
    alert(`Cama asignada al paciente ${patientId}`);
  }

  initializeCharts() {
    const ctx1 = document.getElementById('hospitalMapChart') as HTMLCanvasElement;
    if (ctx1) {
      new Chart(ctx1, {
        type: 'bubble',
        data: {
          datasets: [{
            label: 'Ocupación de Camas',
            data: [
              { x: 10, y: 20, r: 15 },
              { x: 20, y: 30, r: 10 },
              { x: 30, y: 40, r: 20 },
            ],
            backgroundColor: 'rgba(255, 99, 132, 0.2)',
            borderColor: 'rgba(255, 99, 132, 1)',
            borderWidth: 1
          }]
        },
        options: {
          scales: {
            x: {
              beginAtZero: true
            },
            y: {
              beginAtZero: true
            }
          }
        }
      });
    }

    const ctx2 = document.getElementById('bedRequestsChart') as HTMLCanvasElement;
    if (ctx2) {
      new Chart(ctx2, {
        type: 'bar',
        data: {
          labels: ['General', 'UCI', 'Postoperatorio'],
          datasets: [{
            label: 'Solicitudes de Asignación de Camas',
            data: [5, 3, 2],
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
  }
}
