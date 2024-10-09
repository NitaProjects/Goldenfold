import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ControladorCamasInicioComponent } from './controlador-camas-inicio.component';

describe('ControladorCamasInicioComponent', () => {
  let component: ControladorCamasInicioComponent;
  let fixture: ComponentFixture<ControladorCamasInicioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ControladorCamasInicioComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ControladorCamasInicioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
