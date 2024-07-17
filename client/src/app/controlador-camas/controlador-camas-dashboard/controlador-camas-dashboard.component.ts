import { Component, OnInit } from '@angular/core';

interface Bed {
  ubicacion: string;
  estado: string;
  tipo: string;
}

@Component({
  selector: 'app-controlador-camas-dashboard',
  templateUrl: './controlador-camas-dashboard.component.html',
  styleUrls: ['./controlador-camas-dashboard.component.css']
})
export class ControladorCamasDashboardComponent implements OnInit {
  availableBeds: Bed[] = [];
  newBed: Bed = {
    ubicacion: '',
    estado: 'Disponible',
    tipo: ''
  };

  constructor() { }

  ngOnInit(): void {
    // Inicializar con algunos datos de ejemplo
    this.availableBeds = [
      { ubicacion: '7A010101', estado: 'Disponible', tipo: 'General' },
      { ubicacion: '7A010102', estado: 'Disponible', tipo: 'General' },
      { ubicacion: '7A020103', estado: 'Disponible', tipo: 'Intensiva' },
      { ubicacion: '7A030101', estado: 'Disponible', tipo: 'Pediátrica' },
      { ubicacion: '7A030102', estado: 'Disponible', tipo: 'General' }
    ];
  }

  addBed(): void {
    this.availableBeds.push(this.newBed);
    this.newBed = {
      ubicacion: '',
      estado: 'Disponible',
      tipo: ''
    };
  }

  logout(): void {
    // Aquí iría la lógica para cerrar sesión
    console.log('Cerrando sesión...');
  }
}
