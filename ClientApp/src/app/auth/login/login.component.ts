import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule} from '@angular/forms';
import { AuthService } from '../../services/auth.service'; 
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(private fb: FormBuilder, private authService: AuthService) {
    this.loginForm = this.fb.group({
      nombreUsuario: ['', [Validators.required]],
      contrasenya: ['', [Validators.required]]
    });
  }

  onLogin(): void {
    if (this.loginForm.valid) {
      const { nombreUsuario, contrasenya } = this.loginForm.value;
      this.authService.login(nombreUsuario, contrasenya).subscribe({
        next: () => console.log('Login exitoso'),
        error: (err) => alert('Credenciales inválidas. Inténtalo de nuevo.')
      });
    }
  }
}
