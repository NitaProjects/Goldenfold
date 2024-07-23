import { NgModule } from '@angular/core'; // Importa el decorador NgModule de Angular
import { CommonModule } from '@angular/common'; // Importa CommonModule que incluye las directivas comunes de Angular como ngIf y ngFor
import { FormsModule } from '@angular/forms'; // Importa FormsModule para trabajar con formularios en Angular
import { SharedRoutingModule } from './shared-routing.module'; // Importa el módulo de enrutamiento

@NgModule({
  imports: [
    CommonModule, // Importa CommonModule para que las directivas comunes estén disponibles en los componentes de este módulo
    FormsModule, // Importa FormsModule para permitir el uso de formularios en los componentes de este módulo
    SharedRoutingModule // Importa el módulo de enrutamiento
  ],
  declarations: [],
  exports: []
})
export class SharedModule { } // Define y exporta la clase SharedModule, que agrupa componentes, directivas y pipes comunes que pueden ser reutilizados en toda la aplicación
