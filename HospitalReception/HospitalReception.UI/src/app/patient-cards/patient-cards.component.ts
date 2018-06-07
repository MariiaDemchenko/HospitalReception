import { Component, OnInit, Injectable } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DoctorsService } from '../shared/doctors/doctors.service';
import { ImagesService } from '../shared/images/images.service';
import { PatientsService } from '../shared/patients/patients.service';
import { NgbModal, ModalDismissReasons, NgbDateAdapter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Patient } from '../shared/patients/patient.model';

@Injectable()
export class NgbDateNativeAdapter extends NgbDateAdapter<Date> {

  fromModel(date: Date): NgbDateStruct {
    return (date && date.getFullYear) ? { year: date.getFullYear(), month: date.getMonth() + 1, day: date.getDate() } : null;
  }

  toModel(date: NgbDateStruct): Date {
    return date ? new Date(date.year, date.month - 1, date.day) : null;
  }
}

@Component({
  selector: 'app-patient-cards',
  templateUrl: './patient-cards.component.html',
  styleUrls: ['./patient-cards.component.css'],
  providers: [{ provide: NgbDateAdapter, useClass: NgbDateNativeAdapter }]
})
export class PatientCardsComponent implements OnInit {
  Id: FormControl;
  FirstName: FormControl;
  LastName: FormControl;
  Email: FormControl;
  Phone: FormControl;
  BirthDate: FormControl;

  myform: FormGroup;

  patients: any;
  selectedPatient: Patient;
  selectedBirthDate: Date;

  modalReference: any;
  // tslint:disable-next-line:max-line-length
  constructor(private router: Router, private route: ActivatedRoute, private patientsService: PatientsService,
    private imagesService: ImagesService, private modalService: NgbModal) {
  }
  navigate(path) {
    this.router.navigate(['/home', path]);
  }

  addPatient() {
    this.router.navigate(['/patients', 'add']);
  }

  ngOnInit() {
    this.createFormControls();
    this.createForm();
    this.patients = this.patientsService.getAllPatients().subscribe((data: any) => {
      this.patients = data;
    });
  }

  get today() {
    return new Date();
  }

  createFormControls() {
    this.Id = new FormControl('');
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

  createForm() {
    this.myform = new FormGroup({
      Id: this.Id,
      Email: this.Email,
      Phone: this.Phone,
      BirthDate: this.BirthDate,
      FirstName: this.FirstName,
      LastName: this.LastName,
    });
  }

  editPatient() {
    this.BirthDate.setValue(this.selectedBirthDate);
    this.patientsService.editPatient(this.myform.value).subscribe((data: any) => {
      this.modalReference.close();
      this.patientsService.getAllPatients().subscribe((patientsData: any) => this.patients = patientsData);
    });
  }

  openLg(content, id) {
    this.patientsService.getPatientById(id).subscribe((data: any) => {
      this.selectedPatient = data;
      this.Id.setValue(this.selectedPatient.Id);
      this.FirstName.setValue(this.selectedPatient.FirstName);
      this.LastName.setValue(this.selectedPatient.LastName);
      this.Email.setValue(this.selectedPatient.Email);
      this.Phone.setValue(this.selectedPatient.PhoneNumber);
      this.BirthDate.setValue(this.selectedPatient.BirthDate);
      this.selectedBirthDate = new Date(this.selectedPatient.BirthDate);
      this.modalReference = this.modalService.open(content, { size: 'lg' });
    });
  }
}
