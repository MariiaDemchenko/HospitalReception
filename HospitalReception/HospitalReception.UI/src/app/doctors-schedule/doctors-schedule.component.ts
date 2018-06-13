import { Component, OnInit } from '@angular/core';
import { DoctorsService } from '../shared/doctors/doctors.service';
import { ImagesService } from '../shared/images/images.service';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../shared/user.service';
import { Doctor } from '../shared/doctors/doctor.model';

@Component({
  selector: 'app-doctors-schedule',
  templateUrl: './doctors-schedule.component.html',
  styleUrls: ['./doctors-schedule.component.css']
})
export class DoctorsScheduleComponent implements OnInit {

  selectedDoctorId: any;
  doctors: Doctor[];
  selectedDoctor: Doctor;
  id: any;

  // tslint:disable-next-line:max-line-length
  constructor(private router: Router, private activatedRoute: ActivatedRoute, private userService: UserService, private doctorsService: DoctorsService, private imagesService: ImagesService) {
  }

  selectedDoctorChanged(filterVal: any) {
    this.selectedDoctorId = filterVal;
    this.router.navigate(['/doctors-schedule/' + this.selectedDoctorId]);
  }



  getImage(imageId: string) {
    return this.imagesService.getImage(imageId).subscribe((data: any) => {
      return data;
    });
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(paramsId => {
      this.selectedDoctorId = paramsId.id;
      this.doctorsService.getAllDoctors().subscribe((data: Doctor[]) => {
        this.doctors = data;
        this.doctorsService.getDoctorById(this.selectedDoctorId).subscribe((doctor: any) => {
          this.selectedDoctor = doctor;
          this.selectedDoctorId = doctor.Id;
        });
      });
    });
  }
}
