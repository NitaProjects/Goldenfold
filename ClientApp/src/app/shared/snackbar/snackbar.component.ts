import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-snackbar',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="snackbar" [ngClass]="type" *ngIf="showSnackBar">
      <p>{{ message }}</p>
    </div>
  `,
  styleUrls: ['./snackbar.component.css'],
})
export class SnackbarComponent {
  showSnackBar = false; // Cambiado a false por defecto
  message = '';
  type = '';

  showNotification(type: string, message: string) {
    this.type = type; // Cambia el tipo de snackbar
    this.message = message;
    this.showSnackBar = true; // Muestra la notificación

    setTimeout(() => {
      this.showSnackBar = false; // Ocultar después de 3 segundos
    }, 3000);
  }
}
