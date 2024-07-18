import { Component, AfterViewInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { Router } from '@angular/router';

@Component({
  selector: 'app-administrador-sistema-dashboard',
  templateUrl: './administrador-sistema-dashboard.component.html',
  styleUrls: ['./administrador-sistema-dashboard.component.css']
})
export class AdministradorSistemaDashboardComponent implements AfterViewInit {
  
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

  filterUsuarios() {
    const rol = (document.getElementById('filter-rol') as HTMLSelectElement).value;

    const rows = document.querySelectorAll('#usuarios-table-body tr');
    rows.forEach(row => {
      const tableRow = row as HTMLTableRowElement;
      const userRol = tableRow.cells[3].innerText.toLowerCase();

      if (rol && userRol !== rol.toLowerCase()) {
        tableRow.style.display = 'none';
      } else {
        tableRow.style.display = '';
      }
    });
  }

  editUser(userId: number) {
    alert(`Editando usuario ${userId}`);
  }

  initializeCharts() {
    const ctx1 = document.getElementById('userRolesChart') as HTMLCanvasElement;
    if (ctx1) {
      new Chart(ctx1, {
        type: 'pie',
        data: {
          labels: ['Administrador', 'Doctor', 'Enfermera', 'Personal'],
          datasets: [{
            label: 'Distribución de Roles de Usuarios',
            data: [10, 5, 15, 7],
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

    const ctx2 = document.getElementById('userRegistrationChart') as HTMLCanvasElement;
    if (ctx2) {
      new Chart(ctx2, {
        type: 'line',
        data: {
          labels: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
          datasets: [{
            label: 'Registros de Usuarios por Mes',
            data: [5, 10, 8, 6, 7, 9, 15, 20, 17, 14, 12, 10],
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderColor: 'rgba(75, 192, 192, 1)',
            borderWidth: 1
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
