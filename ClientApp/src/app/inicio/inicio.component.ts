import { Component, OnInit, AfterViewInit, OnDestroy } from '@angular/core';

@Component({
  selector: 'app-inicio',
  templateUrl: './inicio.component.html',
  styleUrls: ['./inicio.component.css']
})
export class InicioComponent implements OnInit, AfterViewInit, OnDestroy {

  constructor() { }

  ngOnInit(): void {
    console.log('ngOnInit: InicioComponent initialized');
  }

  ngAfterViewInit(): void {
    console.log('ngAfterViewInit: View initialized');
  }

  ngOnDestroy(): void {
    console.log('ngOnDestroy: InicioComponent destroyed');
  }

}
/*
ngOnInit: Este hook se ejecuta una vez después de que Angular ha inicializado todas las propiedades del componente. 
Es útil para inicializar cualquier lógica que necesite estar lista cuando el componente se cargue.
ngOnDestroy: Este hook se ejecuta justo antes de que Angular destruya el componente. 
Puede ser útil para limpiar recursos o desuscribir observables.
ngAfterViewInit: Este hook se ejecuta después de que Angular ha inicializado la vista del componente. 
Es útil para cualquier lógica que dependa de que la vista esté completamente renderizada.
*/