import { Component, OnInit, Injectable } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DoctorsService } from '../shared/doctors/doctors.service';
import { ImagesService } from '../shared/images/images.service';
import { PatientsService } from '../shared/patients/patients.service';
import { EducationTypesService } from '../shared/education-types/education-types.service';
import { NgbModal, ModalDismissReasons, NgbDateAdapter, NgbDateStruct } from '@ng-bootstrap/ng-bootstrap';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Patient } from '../shared/patients/patient.model';
import { DatePipe } from '@angular/common';
import { MatTableModule } from '@angular/material/table';


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
  MiddleName: FormControl;
  Email: FormControl;
  PhoneNumber: FormControl;
  BirthDate: FormControl;
  Gender: FormControl;
  Education: FormControl;
  Employment: FormControl;
  Organization: FormControl;
  DisabilityGroup: FormControl;
  LocalityName: FormControl;
  InformationSource: FormControl;
  HabitationMember: FormControl;
  Policlinic: FormControl;
  LocalityType: FormControl;
  Region: FormControl;

  myform: FormGroup;

  patients: any;
  displayedColumns: string[] =
  ['FirstName', 'MiddleName', 'LastName', 'Phone', 'Email', 'BirthDate', 'Gender', 'action-view', 'action-edit'];
  genders: any;
  educationTypes: any;
  employmentTypes: any;
  localityTypes: any;
  regions: any;
  disabilityGroups: any;
  informationSources: any;
  habitationMembers: any;
  policlinics: any;

  selectedPatient: Patient;
  selectedBirthDate: Date;

  modalReference: any;
  // tslint:disable-next-line:max-line-length
  constructor(private router: Router, private route: ActivatedRoute, private patientsService: PatientsService,
    private imagesService: ImagesService, private modalService: NgbModal, private educationTypesService: EducationTypesService) {
  }
  navigate(path) {
    this.router.navigate(['/home', path]);
  }

  addPatient() {
    this.router.navigate(['/patients', 'add']);
  }

  ngOnInit() {
    const datePipe = new DatePipe('en-US');
    this.createFormControls();
    this.createForm();
    this.patients = this.patientsService.getAllPatients().subscribe((data: any) => {
      this.patients = data;
      this.patients.forEach(patient => {
          patient.BirthDate = datePipe.transform(patient.BirthDate, 'dd-MM-yyyy');
        });
    });


    this.educationTypesService.getAllGenders().subscribe((data: any) =>
    this.genders = data);

    this.educationTypesService.getAllEducationTypes().subscribe((data: any) =>
    this.educationTypes = data);

    this.educationTypesService.getAllDisabilityGroups().subscribe((data: any) =>
    this.disabilityGroups = data);

    this.educationTypesService.getAllInformationSources().subscribe((data: any) =>
    this.informationSources = data);

    this.educationTypesService.getAllHabitationMembers().subscribe((data: any) =>
    this.habitationMembers = data);

    this.educationTypesService.getAllPoliclinics().subscribe((data: any) =>
    this.policlinics = data);

    this.educationTypesService.getAllRegions().subscribe((data: any) =>
    this.regions = data);

    this.educationTypesService.getAllEmploymentTypes().subscribe((data: any) =>
    this.employmentTypes = data);

    this.educationTypesService.getAllLocalityTypes().subscribe((data: any) =>
    this.localityTypes = data);
  }

  selectedGenderChanged(filterVal: any) {
    this.selectedPatient.GenderId = filterVal;
  }

  selectedEducationTypeChanged(filterVal: any) {
    alert('changed');
    this.selectedPatient.EducationId = filterVal;
    alert(this.selectedPatient.EducationId);
    this.myform.value.EducationId = filterVal;
  }

  selectedDisabilityGroupChanged(filterVal: any) {
    this.selectedPatient.DisabilityGroupId = filterVal;
  }

  selectedInformationSourceChanged(filterVal: any) {
    this.selectedPatient.InformationSourceId = filterVal;
  }

  selectedHabitationMemberChanged(filterVal: any) {
    this.selectedPatient.HabitationMemberId = filterVal;
  }

  selectedPoliclinicChanged(filterVal: any) {
    this.selectedPatient.PoliclinicId = filterVal;
  }

  selectedEmploymentTypeChanged(filterVal: any) {
    this.selectedPatient.EmploymentId = filterVal;
    this.myform.value.EmploymentId = filterVal;
  }

  selectedLocalityTypeChanged(filterVal: any) {
    this.selectedPatient.LocalityTypeId = filterVal;
    this.myform.value.LocalityTypeId = filterVal;
  }

  selectedRegionChanged(filterVal: any) {
    this.selectedPatient.RegionId = filterVal;
    this.myform.value.RegionId = filterVal;
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
    this.MiddleName = new FormControl('', [
      Validators.required
    ]);
    this.Gender = new FormControl('', [
      Validators.required
    ]);
    this.Education = new FormControl('', [
      Validators.required
    ]);
    this.DisabilityGroup = new FormControl('', [
      Validators.required
    ]);
    this.InformationSource = new FormControl('', [
      Validators.required
    ]);
    this.HabitationMember = new FormControl('', [
      Validators.required
    ]);
    this.Policlinic = new FormControl('', [
      Validators.required
    ]);
    this.LocalityName = new FormControl('');
    this.Email = new FormControl('', [
      Validators.required,
      Validators.pattern('[^ @]*@[^ @]*')
    ]);
    this.PhoneNumber = new FormControl('', [
      Validators.required,
      Validators.pattern('[0-9]+')
    ]);
    this.BirthDate = new FormControl('', [
      Validators.required
    ]);
    this.Employment = new FormControl('', [
      Validators.required
    ]);
    this.LocalityType = new FormControl('', [
      Validators.required
    ]);
    this.Organization = new FormControl('', [
      Validators.required
    ]);
    this.Region = new FormControl('', [
      Validators.required
    ]);
  }

  createForm() {
    this.myform = new FormGroup({
      Id: this.Id,
      Email: this.Email,
      PhoneNumber: this.PhoneNumber,
      BirthDate: this.BirthDate,
      FirstName: this.FirstName,
      LastName: this.LastName,
      MiddleName: this.MiddleName,
      LocalityName: this.LocalityName,
      Gender: this.Gender,
      Education: this.Education,
      DisabilityGroup: this.DisabilityGroup,
      InformationSource: this.InformationSource,
      HabitationMember: this.HabitationMember,
      Policlinic: this.Policlinic,
      Employment: this.Employment,
      LocalityType: this.LocalityType,
      Region: this.Region,
    });
  }

  editPatient() {

    const datePipe = new DatePipe('en-US');
    this.selectedPatient.FirstName = this.FirstName.value;
    this.selectedPatient.LastName = this.LastName.value;
    this.selectedPatient.MiddleName = this.MiddleName.value;
    this.selectedPatient.PhoneNumber = this.PhoneNumber.value;
    this.selectedPatient.Email = this.Email.value;
    this.selectedPatient.LocalityName = this.LocalityName.value;

    this.selectedPatient.BirthDate = this.selectedBirthDate.toLocaleDateString();
    alert('edit' + JSON.stringify(this.selectedPatient));
    this.patientsService.editPatient(this.selectedPatient).subscribe((data: any) => {
      this.modalReference.close();
      this.patientsService.getAllPatients().subscribe((patientsData: any) => {
        this.patients = patientsData;
        this.patients.forEach(patient => {
          patient.BirthDate = datePipe.transform(patient.BirthDate, 'dd-MM-yyyy');
        });
      });
    });
  }

  openLg(content, id) {
    this.patientsService.getPatientById(id).subscribe((data: any) => {
      this.selectedPatient = data;
      this.Id.setValue(this.selectedPatient.Id);
      this.FirstName.setValue(this.selectedPatient.FirstName);
      this.MiddleName.setValue(this.selectedPatient.MiddleName);
      this.LastName.setValue(this.selectedPatient.LastName);
      this.Email.setValue(this.selectedPatient.Email);
      this.LocalityName.setValue(this.selectedPatient.LocalityName);
      this.Gender.setValue(this.selectedPatient.Gender);
      this.Education.setValue(this.selectedPatient.Education);
      this.DisabilityGroup.setValue(this.selectedPatient.DisabilityGroup);
      this.InformationSource.setValue(this.selectedPatient.InformationSource);
      this.HabitationMember.setValue(this.selectedPatient.HabitationMember);
      this.Policlinic.setValue(this.selectedPatient.Policlinic);
      this.PhoneNumber.setValue(this.selectedPatient.PhoneNumber);
      this.BirthDate.setValue(this.selectedPatient.BirthDate);
      this.selectedBirthDate = new Date(this.selectedPatient.BirthDate);
      this.BirthDate.setValue(this.selectedPatient.Employment);
      this.Employment.setValue(this.selectedPatient.Employment);
      this.LocalityType.setValue(this.selectedPatient.LocalityType);
      this.Region.setValue(this.selectedPatient.Region);
      this.modalReference = this.modalService.open(content, { size: 'lg' });
    });
  }
}
