import { Component, OnInit } from '@angular/core';
import { NavigationStart, Router } from '@angular/router';
import { UserService } from './shared/user.service';
import { NgModule } from '@angular/core';
import { ViewChild } from '@angular/core';
import { routes } from './app-routing.module';

import 'rxjs/add/operator/filter';
import { Patient } from './shared/patients/patient.model';
import { PatientsService } from './shared/patients/patients.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  userClaims: any;
  title = 'mmm';
  selectedPatientId: any;
  patients: Patient[];

  public topNavLinks: Array<{
    path: string,
    name: string
  }> = [];

  constructor(private router: Router, private userService: UserService, private patientsService: PatientsService) {
    this.userService.authChanged.subscribe((someData?: any) => {
      if (someData) {
        this.userService.getUserClaims().subscribe((data: any) => {
          this.userClaims = data;
        });
      }
    });
  }

  selectedPatientChanged() {
    if (this.selectedPatientId == null) {
      this.selectedPatientId = 1;
    }
    this.router.navigate(['/patients/' + this.selectedPatientId]);
  }

  ngOnInit() {
    this.patientsService.getAllPatients().subscribe((data: any) => this.patients = data);
    this.userService.authChanged.subscribe((someData?: any) => {
      this.userClaims = someData;
    });
  }

  Logout() {
    localStorage.removeItem('userToken');
    this.router.navigate(['/login']);
    this.userService.authChanged.emit(null);
  }
}
