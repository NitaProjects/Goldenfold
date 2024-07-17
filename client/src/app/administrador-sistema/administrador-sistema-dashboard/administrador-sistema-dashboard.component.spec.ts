import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministradorSistemaDashboardComponent } from './administrador-sistema-dashboard.component';

describe('AdministradorSistemaDashboardComponent', () => {
  let component: AdministradorSistemaDashboardComponent;
  let fixture: ComponentFixture<AdministradorSistemaDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdministradorSistemaDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdministradorSistemaDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
