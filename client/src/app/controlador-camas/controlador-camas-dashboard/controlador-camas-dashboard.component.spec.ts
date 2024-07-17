import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ControladorCamasDashboardComponent } from './controlador-camas-dashboard.component';

describe('ControladorCamasDashboardComponent', () => {
  let component: ControladorCamasDashboardComponent;
  let fixture: ComponentFixture<ControladorCamasDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ControladorCamasDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ControladorCamasDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
