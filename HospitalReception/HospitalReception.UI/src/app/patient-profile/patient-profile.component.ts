import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { PatientsService } from '../shared/patients/patients.service';
import { Patient } from '../shared/patients/patient.model';
import { CardRecord } from '../models/card-record';
import { DatePipe } from '@angular/common';
import { PagerService } from '../shared/pager/pager.service';

@Component({
  selector: 'app-patient-profile',
  templateUrl: './patient-profile.component.html',
  styleUrls: ['./patient-profile.component.css']
})
export class PatientProfileComponent implements OnInit {

  userClaims: any;
  id: number;
  patient: Patient;
  appointments: CardRecord[];
  pager: any = {};
  patients: Patient[];

  // paged items
  pagedItems: any[];
  private allItems: any[];

  constructor(private userService: UserService, private router: Router, private route: ActivatedRoute,
    private patientService: PatientsService, private pagerService: PagerService) { }

  setPage(page: number) {
    this.pager = this.pagerService.getPager(this.allItems.length, page);

    this.pagedItems = this.allItems.slice(this.pager.startIndex, this.pager.endIndex + 1);
  }

  ngOnInit() {

    this.patientService.getAllPatients().subscribe((data: any) => {
      this.patients = data;
    });


    const datePipe = new DatePipe('en-US');
    this.userService.getUserClaims().subscribe((data: any) => {
      this.userService.authChanged.emit(data);
      this.userClaims = data;
    });

    this.route.params.subscribe(params => {
      this.id = params['id'];
      this.patientService.getPatientById(this.id).subscribe((data: Patient) => {
        this.patient = data;
        this.patient.BirthDate = datePipe.transform(this.patient.BirthDate, 'dd-MM-yyyy');
      }
      );

      this.patientService.getCardRecords(this.id).subscribe((data: CardRecord[]) => {
        this.appointments = data;
        this.appointments.forEach(appointment => {
          appointment.AppointmentDate = datePipe.transform(appointment.AppointmentDate, 'dd-MM-yyyy HH:mm');
        });
      }
      );
      this.patientService.getCardRecords(this.id)
        .subscribe((data: any) => {
          this.allItems = data;
          this.setPage(1);
        });
    });
  }
}
