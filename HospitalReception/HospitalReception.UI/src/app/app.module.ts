import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { RegisterComponent } from './user/register/register.component';
import { UserService } from './shared/user.service';
import { PagerService } from './shared/pager/pager.service';
import { DoctorsService } from './shared/doctors/doctors.service';
import { ImagesService } from './shared/images/images.service';
import { DepartmentsService } from './shared/departments/departments.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import { routerConfig } from '../router.config';
import { DoctorsComponent } from './doctors/doctors.component';
import { AboutComponent } from './about/about.component';
import { DoctorProfilesComponent } from './doctor-profiles/doctor-profiles.component';
import { SideMenuComponent } from './categories-menu/categories-menu.component';
import { DoctorProfileComponent } from './doctor-profile/doctor-profile.component';
import { RouterModule } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { SignInComponent } from './user/sign-in/sign-in.component';
import { UserComponent } from './user/user.component';
import { AuthGuard } from './auth/auth.guard';
import { AuthInterceptor } from './auth/auth.interceptor';
import { SignOutComponent } from './user/sign-out/sign-out.component';
import { DoctorProfileEditComponent } from './doctor-profile-edit/doctor-profile-edit.component';
import { PatientCardsComponent } from './patient-cards/patient-cards.component';
import { PatientsComponent } from './patients/patients.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { PatientEditCardComponent } from './patient-edit-card/patient-edit-card.component';
import { DoctorsScheduleComponent } from './doctors-schedule/doctors-schedule.component';
import { SchedulerComponent } from './doctors-schedule/scheduler.component';
// import { HttpClientModule } from '@angular/http';
import { EventService } from './services/event.service';
import { AlertsModule } from 'angular-alert-module';
import { NgSelectModule } from '@ng-select/ng-select';
import { PatientProfileComponent } from './patient-profile/patient-profile.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { MatRadioModule } from '@angular/material/radio';


@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    HomeComponent,
    DoctorsComponent,
    AboutComponent,
    DoctorProfilesComponent,
    SideMenuComponent,
    DoctorProfileComponent,
    SignInComponent,
    UserComponent,
    SignOutComponent,
    DoctorProfileEditComponent,
    PatientCardsComponent,
    PatientsComponent,
    PatientEditCardComponent,
    DoctorsScheduleComponent,
    SchedulerComponent,
    PatientProfileComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routerConfig),
    NgbModule,
    AlertsModule.forRoot(),
    NgSelectModule,
    BrowserAnimationsModule,
    NgbModule,
    MatTableModule,
    MatTabsModule,
    MDBBootstrapModule.forRoot(),
    MatRadioModule
  ],
  // tslint:disable-next-line:max-line-length
  providers: [UserService, DoctorsService, ImagesService, DepartmentsService, EventService, AuthGuard, PagerService, { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor,  multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
