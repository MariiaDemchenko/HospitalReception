import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientEditCardComponent } from './patient-edit-card.component';

describe('PatientEditCardComponent', () => {
  let component: PatientEditCardComponent;
  let fixture: ComponentFixture<PatientEditCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PatientEditCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PatientEditCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
