import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.registerForm = this.fb.group({
      nombre: ['', [Validators.required, Validators.minLength(3)]],
      username: ['', [Validators.required, Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.minLength(3)]],
      confirmPassword: ['', [Validators.required, Validators.minLength(3)]],
      idRol: [null, [Validators.required]] // Añadir idRol al formulario
    });
  }

  onRegister() {
    if (this.registerForm.valid) {
      const { nombre, username, password, confirmPassword, idRol } = this.registerForm.value;
      if (password !== confirmPassword) {
        alert('Las contraseñas no coinciden.');
        return;
      }
      this.authService.register(nombre, username, password, idRol).subscribe({
        next: () => {
          alert('Registro exitoso. Ahora puedes iniciar sesión.');
          this.router.navigate(['/login']);
        },
        error: (err) => {
          alert('Error al registrarse. Inténtalo de nuevo.');
        }
      });
    }
  }
}
