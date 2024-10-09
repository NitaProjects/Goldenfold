import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5076/api/auth'; 

  constructor(private http: HttpClient, private router: Router) {}

  // Método para iniciar sesión
  login(nombreUsuario: string, contrasenya: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, { nombreUsuario, contrasenya }).pipe(
      tap(response => {
        if (response?.Token) {
          localStorage.setItem('token', response.Token);

          // Suponiendo que la respuesta tiene información sobre el rol del usuario
          const rol = response.rol;

          // Redirigir al dashboard correspondiente según el rol
          switch (rol) {
            case 1: // Administrativo
              this.router.navigate(['/administrativo-dashboard']);
              break;
            case 2: // Médico
              this.router.navigate(['/medico-dashboard']);
              break;
            case 3: // Controlador de Camas
              this.router.navigate(['/controlador-camas-dashboard']);
              break;
            case 4: // Administrador de Sistema
              this.router.navigate(['/administrador-sistema-dashboard']);
              break;
            default:
              this.router.navigate(['/inicio']); // En caso de un rol desconocido, redirigir al inicio
          }
        }
      })
    );
  }

  // Método para cerrar sesión
  logout(): void {
    localStorage.removeItem('token');
    this.router.navigate(['/login']); // Redirige al login al cerrar sesión
  }

  // Método para verificar si el usuario está autenticado
  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  // Método para obtener el token JWT almacenado
  getToken(): string | null {
    return localStorage.getItem('token');
  }

}
