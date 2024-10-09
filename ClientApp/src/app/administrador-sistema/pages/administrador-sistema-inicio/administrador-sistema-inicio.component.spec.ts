import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministradorSistemaInicioComponent } from './administrador-sistema-inicio.component';

describe('AdministradorSistemaInicioComponent', () => {
  let component: AdministradorSistemaInicioComponent;
  let fixture: ComponentFixture<AdministradorSistemaInicioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdministradorSistemaInicioComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdministradorSistemaInicioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
