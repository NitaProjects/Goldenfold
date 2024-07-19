import { Component, AfterViewInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { Router } from '@angular/router';

interface Bed {
  id: number;
  type: string;
  available: boolean;
}

interface AdmissionRequest {
  id: number;
  patientName: string;
  patientId: number;
  requestedBedType: string;
}

@Component({
  selector: 'app-controlador-camas-dashboard',
  templateUrl: './controlador-camas-dashboard.component.html',
  styleUrls: ['./controlador-camas-dashboard.component.css']
})
export class ControladorCamasDashboardComponent implements AfterViewInit {
  beds: Bed[] = [
    { id: 1, type: 'General', available: true },
    { id: 2, type: 'General', available: false },
    { id: 3, type: 'UCI', available: true },
    { id: 4, type: 'Postoperatorio', available: true }
  ];

  admissionRequests: AdmissionRequest[] = [
    { id: 1, patientName: 'Juan Garcia', patientId: 101, requestedBedType: 'General' },
    { id: 2, patientName: 'Maria Lopez', patientId: 102, requestedBedType: 'UCI' }
  ];

  selectedRequest: AdmissionRequest | null = null;
  selectedBedId: number | null = null;

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

  toggleAssignSection(request: AdmissionRequest) {
    if (this.selectedRequest && this.selectedRequest.id === request.id) {
      this.selectedRequest = null; // Ocultar la sección si se hace clic nuevamente
      this.selectedBedId = null;
    } else {
      this.selectedRequest = request; // Mostrar la sección con la solicitud seleccionada
      this.selectedBedId = null;
    }
  }

  assignBed(event: Event) {
    event.preventDefault();
    if (this.selectedRequest && this.selectedBedId !== null) {
      const bed = this.beds.find(b => b.id === this.selectedBedId);
      if (bed) {
        bed.available = false;
        alert(`Cama ${bed.id} asignada al paciente ${this.selectedRequest.patientName}`);
        this.selectedRequest = null;
        this.selectedBedId = null;
        this.initializeCharts(); // Actualizar los gráficos después de asignar la cama
      }
    }
  }

  getAvailableBeds(bedType: string) {
    return this.beds.filter(bed => bed.type === bedType && bed.available);
  }

  initializeCharts() {
    // Gráfico de Disponibilidad de Camas por Tipo
    const ctx1 = document.getElementById('bedAvailabilityChart') as HTMLCanvasElement;
    if (ctx1) {
      new Chart(ctx1, {
        type: 'bar',
        data: {
          labels: ['General', 'UCI', 'Postoperatorio'],
          datasets: [{
            label: 'Camas Disponibles',
            data: this.getAvailableBedsCount(),
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderColor: 'rgba(75, 192, 192, 1)',
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

    // Gráfico de Ocupación de Camas por Tipo
    const ctx2 = document.getElementById('bedOccupancyChart') as HTMLCanvasElement;
    if (ctx2) {
      new Chart(ctx2, {
        type: 'pie',
        data: {
          labels: ['General', 'UCI', 'Postoperatorio'],
          datasets: [{
            label: 'Camas Ocupadas',
            data: this.getOccupiedBedsCount(),
            backgroundColor: [
              'rgba(255, 99, 132, 0.2)',
              'rgba(54, 162, 235, 0.2)',
              'rgba(255, 206, 86, 0.2)'
            ],
            borderColor: [
              'rgba(255, 99, 132, 1)',
              'rgba(54, 162, 235, 1)',
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

  getAvailableBedsCount(): number[] {
    const bedTypes = ['General', 'UCI', 'Postoperatorio'];
    return bedTypes.map(type => this.beds.filter(bed => bed.type === type && bed.available).length);
  }

  getOccupiedBedsCount(): number[] {
    const bedTypes = ['General', 'UCI', 'Postoperatorio'];
    return bedTypes.map(type => this.beds.filter(bed => bed.type === type && !bed.available).length);
  }
}
