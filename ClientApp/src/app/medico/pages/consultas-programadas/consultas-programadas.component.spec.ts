import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConsultasProgramadasComponent } from './consultas-programadas.component';

describe('ConsultasProgramadasComponent', () => {
  let component: ConsultasProgramadasComponent;
  let fixture: ComponentFixture<ConsultasProgramadasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ConsultasProgramadasComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ConsultasProgramadasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
