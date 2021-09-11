import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { PatientsService } from '../shared/patients/patients.service';
import { Patient } from '../shared/patients/patient.model';
import { CardRecord } from '../models/card-record';
import { DatePipe } from '@angular/common';
import { PagerService } from '../shared/pager/pager.service';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatRadioModule } from '@angular/material/radio';

@Component({
  selector: 'app-patient-profile',
  templateUrl: './patient-profile.component.html',
  styleUrls: ['./patient-profile.component.css']
})
export class PatientProfileComponent implements OnInit {

  public chartType = 'line';

  public chart = 'Systolic Blood Pressure';

  public charts = ['Systolic Blood Pressure', 'Diastolic Blood Pressure'];

  public chartDatasets: Array<any>;

  public chartLabels: Array<any>;

  public chartColors: Array<any> = [
    {
      backgroundColor: 'rgba(105, 0, 132, .2)',
      borderColor: 'rgba(200, 99, 132, .7)',
      borderWidth: 2,
    },
    {
      backgroundColor: 'rgba(0, 137, 132, .2)',
      borderColor: 'rgba(0, 10, 130, .7)',
      borderWidth: 2,
    }
  ];

  public chartOptions: any = {
    responsive: true
  };

  userClaims: any;
  id: number;
  patient: Patient;
  appointments: CardRecord[];
  pager: any = {};
  patients: Patient[];
  atherosclerosisRisk: number;
  infarctionRisk = 0.32;
  displayedColumns: string[] = ['Label', 'Value'];

  // paged items
  pagedItems: any[];
  private allItems: any[];

  constructor(private userService: UserService, private router: Router, private route: ActivatedRoute,
    private patientService: PatientsService, private pagerService: PagerService) { }

  setPage(page: number) {
    this.pager = this.pagerService.getPager(this.allItems.length, page);
    this.pagedItems = this.allItems.slice(this.pager.startIndex, this.pager.endIndex + 1);
  }


  predictRisk() {
    this.patientService.predictRisk(this.patient).subscribe((data: number) => {
      this.atherosclerosisRisk = data;
    });
  }

  radioChange() {
    const datePipe = new DatePipe('en-US');
    console.log(this.chart);
    if (this.chart === 'Systolic Blood Pressure') {
      this.chartDatasets = [
        { data: this.patient.SBPla.map(point => point.Value), label: 'Left Arm' },
        { data: this.patient.SBPra.map(point => point.Value), label: 'Right Arm' },
      ];
      this.chartLabels = this.patient.SBPla.map(point => datePipe.transform(point.MeasurementDate, 'MMM d'));

    } else {
      this.chartDatasets = [
        { data: this.patient.DBPla.map(point => point.Value), label: 'Left Arm' },
        { data: this.patient.DBPra.map(point => point.Value), label: 'Right Arm' },
      ];
      this.chartLabels = this.patient.DBPla.map(point => datePipe.transform(point.MeasurementDate, 'MMM d'));

    }
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

        if (this.chart === 'Systolic Blood Pressure') {
          this.chartDatasets = [
            { data: this.patient.SBPla.map(point => point.Value), label: 'Left' },
            { data: this.patient.SBPra.map(point => point.Value), label: 'Right' },
          ];
          this.chartLabels = this.patient.SBPla.map(point => datePipe.transform(point.MeasurementDate, 'MMM d'));

        } else {
          this.chartDatasets = [
            { data: this.patient.DBPla.map(point => point.Value), label: 'Left' },
            { data: this.patient.DBPra.map(point => point.Value), label: 'Right' },
          ];
          this.chartLabels = this.patient.SBPla.map(point => datePipe.transform(point.MeasurementDate, 'MMM d'));

        }
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
