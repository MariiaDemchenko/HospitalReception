import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorProfilesComponent } from './doctor-profiles.component';

describe('CourseCardsComponent', () => {
  let component: DoctorProfilesComponent;
  let fixture: ComponentFixture<DoctorProfilesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [DoctorProfilesComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoctorProfilesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
