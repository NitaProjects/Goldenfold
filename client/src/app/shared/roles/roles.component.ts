import { Component, OnInit } from '@angular/core';
import { ApiService, Rol } from '../../services/api.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-roles',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent implements OnInit {
  roles: Rol[] = [];
  nuevoRol: Rol = { IdRol: 0, NombreRol: '' };

  constructor(private apiService: ApiService) { }

  ngOnInit(): void {
    this.obtenerRoles();
  }

  obtenerRoles(): void {
    this.apiService.getRoles().subscribe({
      next: (data: Rol[]) => {
        this.roles = data;
      },
      error: (error: any) => {
        console.error('Error al obtener los roles', error);
      }
    });
  }

  agregarRol(): void {
    this.apiService.addRol(this.nuevoRol).subscribe({
      next: (rol: Rol) => {
        this.roles.push(rol);
        this.nuevoRol = { IdRol: 0, NombreRol: '' }; // Reset form
      },
      error: (error: any) => {
        console.error('Error al agregar el rol', error);
      }
    });
  }

  actualizarRol(rol: Rol): void {
    this.apiService.updateRol(rol).subscribe({
      next: (rolActualizado: Rol) => {
        const index = this.roles.findIndex(r => r.IdRol === rolActualizado.IdRol);
        if (index !== -1) {
          this.roles[index] = rolActualizado;
        }
      },
      error: (error: any) => {
        console.error('Error al actualizar el rol', error);
      }
    });
  }

  borrarRol(id: number): void {
    this.apiService.deleteRol(id).subscribe({
      next: () => {
        this.roles = this.roles.filter(r => r.IdRol !== id);
      },
      error: (error: any) => {
        console.error('Error al borrar el rol', error);
      }
    });
  }
}
