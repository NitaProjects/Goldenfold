import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogFormularioComponent } from './dialog-formulario.component';

describe('DialogFormularioComponent', () => {
  let component: DialogFormularioComponent;
  let fixture: ComponentFixture<DialogFormularioComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DialogFormularioComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DialogFormularioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
