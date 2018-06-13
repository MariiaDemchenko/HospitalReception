import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Doctor } from '../shared/doctors/doctor.model';
import { DoctorsService } from '../shared/doctors/doctors.service';
import { HttpClient } from '@angular/common/http';
import { DepartmentsService } from '../shared/departments/departments.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-doctor-profile-edit',
  templateUrl: './doctor-profile-edit.component.html',
  styleUrls: ['./doctor-profile-edit.component.css']
})
export class DoctorProfileEditComponent implements OnInit {
  id: number;
  private sub: any;
  doctor: Doctor;
  imageChanged: boolean;
  selectedFile: File = null;
  tmpPath: any;
  initTmpPath: any;
  add: boolean;
  departments: any;
  selectedDepartment: any;

  FirstName: FormControl;
  LastName: FormControl;

  myform: FormGroup;
  Email: FormControl;

  // tslint:disable-next-line:max-line-length
  constructor(private router: Router, private route: ActivatedRoute, private doctorsService: DoctorsService,
    private departmentService: DepartmentsService, private http: HttpClient) {
  }

  onFileSelected(event) {
    this.selectedFile = <File>event.target.files[0];
    this.tmpPath = event.target.result;

    if (event.target.files && event.target.files[0]) {
      const reader = new FileReader();

      reader.onload = (e: any) => {
        this.tmpPath = e.target.result;
      };
      reader.readAsDataURL(event.target.files[0]);
    }
  }

  onUpload() {
    const fd = new FormData();
    if (this.tmpPath !== this.initTmpPath) {
      fd.append('image', this.selectedFile, this.selectedFile.name);
    }

    fd.append('firstName', this.FirstName.value);
    fd.append('lastName', this.LastName.value);
    fd.append('departmentId', this.doctor.DepartmentId);
    if (this.add) {
      this.doctorsService.addDoctor(fd).subscribe((data: any) =>
        this.router.navigate(['/home'])
      );
    } else {
      this.doctorsService.editDoctor(fd, this.doctor.Id).subscribe((data: any) =>
      this.router.navigate(['/home'])
    );
    }
  }

  selectedDepartmentChanged(filterVal: any) {
    this.doctor.DepartmentId = filterVal;
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      this.id = params['id'];
    });

    this.createFormControls();
    this.createForm();

    this.departmentService.getAllDepartments().subscribe((data: any) =>
      this.departments = data);

    if (this.id === undefined) {
      this.add = true;
      this.tmpPath = 'http://localhost:55434/api/images/empty';
      this.initTmpPath = this.tmpPath;
      this.doctor = new Doctor();
      this.doctor.DepartmentId = '1';
    } else {
      this.add = false;
      this.doctorsService.getDoctorById(this.id).subscribe((data: any) => {
        this.doctor = data;
        this.tmpPath = 'http://localhost:55434/api/images/' + this.doctor.ImageId;
        this.FirstName.setValue(this.doctor.FirstName);
        this.LastName.setValue(this.doctor.LastName);
        this.initTmpPath = this.tmpPath;
      });
    }
  }

  createFormControls() {
    this.FirstName = new FormControl('', [
      Validators.required
    ]);
    this.LastName = new FormControl('', [
      Validators.required
    ]);
  }

  createForm() {
    this.myform = new FormGroup({
      FirstName: this.FirstName,
      LastName: this.LastName
    });
  }
}
