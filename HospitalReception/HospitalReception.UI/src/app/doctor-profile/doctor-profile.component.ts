import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DoctorsService } from '../shared/doctors/doctors.service';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { Doctor } from '../shared/doctors/doctor.model';
import { UserService } from '../shared/user.service';

@Component({
  selector: 'app-course-category',
  templateUrl: './doctor-profile.component.html',
  styleUrls: ['./doctor-profile.component.css']
})

export class DoctorProfileComponent implements OnInit {
  id: number;
  private sub: any;
  doctor: Doctor;
  selectedFile: File = null;
  userClaims: any;
  constructor(private router: Router, private route: ActivatedRoute, private doctorsService: DoctorsService,
    private http: HttpClient, private userService: UserService) { }

  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
  }

  navigate(id: string) {
    this.router.navigate(['/home/edit', id]);
  }

  navigateToAppointment(id: string) {
    this.router.navigate(['/doctors-schedule', id]);
  }

  onUpload() {
    const fd = new FormData();
    fd.append('image', this.selectedFile, this.selectedFile.name);
    fd.append('firstName', this.doctor.FirstName);
    fd.append('lastName', this.doctor.LastName);
    fd.append('departmentId', this.doctor.DepartmentId);
    this.doctorsService.addDoctor(fd).subscribe();
  }

  ngOnInit() {
    this.userService.getUserClaims().subscribe((data: any) => {
      this.userService.authChanged.emit(data);
      this.userClaims = data;
    });

    this.route.params.subscribe(params => {
      this.id = params['id'];
    });

    this.doctorsService.getDoctorById(this.id).subscribe((data: any) => {
      this.doctor = data;
    });
  }
}
