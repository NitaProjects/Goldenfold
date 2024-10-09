import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-controlador-camas-dashboard',
  templateUrl: './controlador-camas-dashboard.component.html',
  styleUrls: ['./controlador-camas-dashboard.component.css']
})
export class ControladorCamasDashboardComponent {
  constructor(private router: Router) {}

  logout() {
    alert('Sesión cerrada');
    this.router.navigate(["/inicio"]);
  }
}
