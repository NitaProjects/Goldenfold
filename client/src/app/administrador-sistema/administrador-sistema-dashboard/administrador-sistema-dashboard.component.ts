import { Component, OnInit } from '@angular/core';

interface Role {
  name: string;
  permissions: string[];
}

@Component({
  selector: 'app-administrador-sistema-dashboard',
  templateUrl: './administrador-sistema-dashboard.component.html',
  styleUrls: ['./administrador-sistema-dashboard.component.css']
})
export class AdministradorSistemaDashboardComponent implements OnInit {
  roles: Role[] = [];
  newRole: Role = {
    name: '',
    permissions: []
  };
  selectedRole: Role | null = null;

  constructor() { }

  ngOnInit(): void {
    // Inicializar con algunos datos de ejemplo
    this.roles = [
      { name: 'Administrador', permissions: ['Crear', 'Editar', 'Eliminar'] },
      { name: 'Usuario', permissions: ['Ver', 'Comentar'] },
    ];
  }

  addRole(): void {
    this.roles.push({ ...this.newRole });
    this.newRole = { name: '', permissions: [] };
  }

  editRole(role: Role): void {
    this.selectedRole = { ...role };
  }

  updateRole(): void {
    if (this.selectedRole) {
      const index = this.roles.findIndex(r => r.name === this.selectedRole!.name);
      if (index !== -1) {
        this.roles[index] = { ...this.selectedRole };
        this.selectedRole = null;
      }
    }
  }

  deleteRole(role: Role): void {
    this.roles = this.roles.filter(r => r !== role);
  }

  logout(): void {
    // Aquí iría la lógica para cerrar sesión
    console.log('Cerrando sesión...');
  }
}
