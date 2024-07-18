import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PacientesIngresadosComponent } from './pacientes-ingresados.component';

describe('PacientesIngresadosComponent', () => {
  let component: PacientesIngresadosComponent;
  let fixture: ComponentFixture<PacientesIngresadosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PacientesIngresadosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PacientesIngresadosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
