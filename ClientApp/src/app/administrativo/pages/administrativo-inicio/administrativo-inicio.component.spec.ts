import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministrativoInicioComponent } from './administrativo-inicio.component';

describe('AdministrativoInicioComponent', () => {
  let component: AdministrativoInicioComponent;
  let fixture: ComponentFixture<AdministrativoInicioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AdministrativoInicioComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdministrativoInicioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
