import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HistorialAltasComponent } from './historial-altas.component';

describe('HistorialAltasComponent', () => {
  let component: HistorialAltasComponent;
  let fixture: ComponentFixture<HistorialAltasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HistorialAltasComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(HistorialAltasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
