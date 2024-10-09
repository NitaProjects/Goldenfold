import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdministrativoDashboardComponent } from './administrativo-dashboard.component';

describe('AdministrativoDashboardComponent', () => {
  let component: AdministrativoDashboardComponent;
  let fixture: ComponentFixture<AdministrativoDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AdministrativoDashboardComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AdministrativoDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
