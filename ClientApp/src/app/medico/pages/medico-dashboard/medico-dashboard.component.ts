import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-medico-dashboard',
  templateUrl: './medico-dashboard.component.html',
  styleUrls: ['./medico-dashboard.component.css']
})
export class MedicoDashboardComponent {
  constructor(private router: Router) {}

  logout() {
    alert('Sesión cerrada');
    this.router.navigate(["/inicio"]);
  }
}
