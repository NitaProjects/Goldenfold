import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AsignarCamaComponent } from './asignar-cama.component';

describe('AsignarCamaComponent', () => {
  let component: AsignarCamaComponent;
  let fixture: ComponentFixture<AsignarCamaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AsignarCamaComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AsignarCamaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
