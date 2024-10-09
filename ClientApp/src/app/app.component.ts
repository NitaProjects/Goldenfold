import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { KeycloakService } from 'keycloak-angular';
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NavbarComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

  title = "Hospital Goldenfold";
  authenticated = false;
  isUser = false;
  isAdmin = false;

  constructor(private readonly keycloak: KeycloakService) { }

  async ngOnInit(): Promise<void> {
    /*
    try {
      await this.keycloak.init({
        config: {
          url: 'https://login.oscarrovira.com',
          realm: 'GoGoTron',
          clientId: 'goldenfold-frontend'
        },

        initOptions: {
          onLoad: 'login-required',
          checkLoginIframe: false

        }

      });

      this.authenticated = this.keycloak.isLoggedIn();

      if (this.authenticated) {
        const roles = this.keycloak.getUserRoles();
        this.isUser = roles.includes('USER');
        this.isAdmin = roles.includes('ADMIN');

      }

    } catch (error) {
      console.error('La inicialización de Keycloak ha fallado.', error);

    }
      */
  }
  
  login() {
    this.keycloak.login();
  }

  logout() {
    this.keycloak.logout();
  }
 
 
}
 