import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EstadoCamasComponent } from './estado-camas.component';

describe('EstadoCamasComponent', () => {
  let component: EstadoCamasComponent;
  let fixture: ComponentFixture<EstadoCamasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EstadoCamasComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EstadoCamasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
