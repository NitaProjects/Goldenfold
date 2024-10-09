import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-administrativo-dashboard',
  templateUrl: './administrativo-dashboard.component.html',
  styleUrls: ['./administrativo-dashboard.component.css']
})
export class AdministrativoDashboardComponent {
  constructor(private router: Router) {}

  logout() {
    alert('Sesión cerrada');
    this.router.navigate(["/inicio"]);
  }
}
