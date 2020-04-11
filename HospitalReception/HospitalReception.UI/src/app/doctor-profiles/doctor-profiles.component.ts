import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DoctorsService } from '../shared/doctors/doctors.service';
import { ImagesService } from '../shared/images/images.service';
import { NgModule } from '@angular/core';
import {Doctor} from '../shared/doctors/doctor.model';

@Component({
  selector: 'app-doctor-profiles',
  templateUrl: './doctor-profiles.component.html',
  styleUrls: ['./doctor-profiles.component.css']
})

export class DoctorProfilesComponent implements OnInit {
  doctors: Doctor[];
  // tslint:disable-next-line:max-line-length
  constructor(private router: Router, private route: ActivatedRoute, private doctorsService: DoctorsService, private imagesService: ImagesService) {
  }
  navigate(path) {
    this.router.navigate(['/home', path]);
  }

  addDoctor() {
    this.router.navigate(['/home', 'add']);
  }

  ngOnInit() {
    this.doctorsService.getAllDoctors().subscribe( 
      doctors => this.doctors = doctors
    );
  }
}
