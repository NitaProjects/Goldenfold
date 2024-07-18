import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PacientesRegistradosComponent } from './pacientes-registrados.component';

describe('PacientesRegistradosComponent', () => {
  let component: PacientesRegistradosComponent;
  let fixture: ComponentFixture<PacientesRegistradosComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PacientesRegistradosComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PacientesRegistradosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
