import { Component, OnInit, Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Patient } from '../shared/patients/patient.model';
import { PatientsService } from '../shared/patients/patients.service';
import { NgbDateAdapter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';

@Injectable()
export class NgbDateNativeAdapter extends NgbDateAdapter<Date> {

  fromModel(date: Date): NgbDateStruct {
    return (date && date.getFullYear) ? { year: date.getFullYear(), month: date.getMonth(), day: date.getDate() } : null;
  }

  toModel(date: NgbDateStruct): Date {
    return date ? new Date(date.year, date.month, date.day) : null;
  }
}

@Component({
  selector: 'app-patient-edit-card',
  templateUrl: './patient-edit-card.component.html',
  styleUrls: ['./patient-edit-card.component.css'],
  providers: [{ provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }]
})
export class PatientEditCardComponent implements OnInit {

  constructor(private patientsService: PatientsService, private router: Router) {
    this.selectedBirthDate = new Date(2018, 2, 2);
  }

  FirstName: FormControl;
  LastName: FormControl;
  Email: FormControl;
  Phone: FormControl;
  BirthDate: FormControl;

  myform: FormGroup;

  patients: any;
  selectedPatient: Patient;
  selectedBirthDate: Date;

  ngOnInit() {
    this.createFormControls();
    this.createForm();
  }

  createFormControls() {
    this.FirstName = new FormControl('', [
      Validators.required
    ]);
    this.LastName = new FormControl('', [
      Validators.required
    ]);
    this.Email = new FormControl('', [
      Validators.required,
      Validators.email
    ]);
    this.Phone = new FormControl('', [
      Validators.required
    ]);
    this.BirthDate = new FormControl('', [
      Validators.required
    ]);
  }

  addPatient() {
    this.BirthDate.setValue(this.selectedBirthDate);
    this.patientsService.addPatient(this.myform.value).subscribe((data: any) => this.router.navigate(['/patients']));
  }

  createForm() {
    this.myform = new FormGroup({
      Email: this.Email,
      Phone: this.Phone,
      BirthDate: this.BirthDate,
      FirstName: this.FirstName,
      LastName: this.LastName,
    });
  }
}
