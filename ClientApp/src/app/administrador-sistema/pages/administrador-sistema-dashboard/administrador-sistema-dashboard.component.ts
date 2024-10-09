import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-administratidor-sistema-dashboard',
  templateUrl: './administrador-sistema-dashboard.component.html',
  styleUrls: ['./administrador-sistema-dashboard.component.css']
})
export class AdministradorSistemaDashboardComponent {
  constructor(private router: Router) {}

  logout() {
    alert('Sesión cerrada');
    this.router.navigate(["/inicio"]);
  }

}
