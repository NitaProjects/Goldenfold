import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { RouterLinkActive, RouterLink } from '@angular/router';
import { LoginComponent } from '../../auth/login/login.component';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, LoginComponent],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  mostrarFormularioLogin = false;

  constructor(private authService: AuthService) {}

  mostrarLogin() {
    this.mostrarFormularioLogin = true;
  }

  cerrarLogin() {
    this.mostrarFormularioLogin = false;
  }
}
