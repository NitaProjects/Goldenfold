import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { KeycloakAngularModule } from 'keycloak-angular';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './shared/shared.module';  // Importa el SharedModule

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    AppRoutingModule,
    KeycloakAngularModule,
    BrowserAnimationsModule,
    SharedModule  // Asegúrate de incluir el SharedModule aquí
  ],
  providers: [],
  bootstrap: [AppComponent]  // Asegúrate de hacer bootstrap AppComponent
})
export class AppModule { }
