import { Component, OnInit, ViewChild } from '@angular/core';
import { DoctorsService } from '../shared/doctors/doctors.service';
import { ImagesService } from '../shared/images/images.service';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../shared/user.service';
import { Doctor } from '../shared/doctors/doctor.model';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { Observable, Subject, merge } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-doctors-schedule',
  templateUrl: './doctors-schedule.component.html',
  styleUrls: ['./doctors-schedule.component.css']
})
export class DoctorsScheduleComponent implements OnInit {

  selectedDoctorId: any;
  doctors: Doctor[];
  doctorsObservable: Observable<Doctor[]>;
  selectedDoctor: Doctor;
  id: any;

  model: any;

  // tslint:disable-next-line:max-line-length
  constructor(private router: Router, private activatedRoute: ActivatedRoute, private userService: UserService, private doctorsService: DoctorsService, private imagesService: ImagesService) {
  }

  selectedDoctorChanged() {
    if (this.selectedDoctorId == null) {
      this.selectedDoctorId = 1;
    }
    this.router.navigate(['/doctors-schedule/' + this.selectedDoctorId]);
  }

  getImage(imageId: string) {
    return this.imagesService.getImage(imageId).subscribe((data: any) => {
      return data;
    });
  }

  ngOnInit() {
    this.doctorsService.getAllDoctors().subscribe((data: any) => {
      this.doctors = data;
    });
    this.userService.getUserClaims().subscribe((data: any) => {
      this.userService.authChanged.emit(data);
    });
    this.activatedRoute.params.subscribe(paramsId => {
      this.selectedDoctorId = paramsId.id;
      this.doctorsService.getAllDoctors().subscribe((data: any) => {
        this.doctors = data;
        this.doctorsService.getDoctorById(this.selectedDoctorId).subscribe((doctor: any) => {
          this.selectedDoctor = doctor;
          this.selectedDoctorId = doctor.Id;
        });
      });
    });
  }
}
