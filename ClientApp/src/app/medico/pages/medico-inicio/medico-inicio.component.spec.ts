import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicoInicioComponent } from './medico-inicio.component';

describe('MedicoInicioComponent', () => {
  let component: MedicoInicioComponent;
  let fixture: ComponentFixture<MedicoInicioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MedicoInicioComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MedicoInicioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
