import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SolicitudesIngresoComponent } from './solicitudes-ingreso.component';

describe('SolicitudesIngresoComponent', () => {
  let component: SolicitudesIngresoComponent;
  let fixture: ComponentFixture<SolicitudesIngresoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SolicitudesIngresoComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SolicitudesIngresoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
