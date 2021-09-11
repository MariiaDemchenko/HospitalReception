import { Routes } from '@angular/router';
import { HomeComponent } from './app/home/home.component';
import { AboutComponent } from './app/about/about.component';
import { DoctorsComponent } from './app/doctors/doctors.component';
import { DoctorProfilesComponent } from './app/doctor-profiles/doctor-profiles.component';
import { SideMenuComponent } from './app/categories-menu/categories-menu.component';
import { DoctorProfileComponent } from './app/doctor-profile/doctor-profile.component';
import { RegisterComponent } from './app/user/register/register.component';
import { SignInComponent } from './app/user/sign-in/sign-in.component';
import { SignOutComponent } from './app/user/sign-out/sign-out.component';
import { AuthGuard } from './app/auth/auth.guard';
import { DoctorProfileEditComponent } from './app/doctor-profile-edit/doctor-profile-edit.component';
import { PatientCardsComponent } from './app/patient-cards/patient-cards.component';
import { PatientsComponent } from './app/patients/patients.component';
import { PatientEditCardComponent } from './app/patient-edit-card/patient-edit-card.component';
import { DoctorsScheduleComponent } from './app/doctors-schedule/doctors-schedule.component';
import { PatientProfileComponent } from './app/patient-profile/patient-profile.component';

export const routerConfig: Routes = [
    {
        path: 'home',
        component: HomeComponent,
        children: [
            {
                path: '',
                component: DoctorProfilesComponent
            },
            {
                path: 'add',
                component: DoctorProfileEditComponent,
                canActivate: [AuthGuard]
            },
            {
                path: ':id',
                component: DoctorProfileComponent,
            },
            {
                path: 'edit/:id',
                component: DoctorProfileEditComponent,
                canActivate: [AuthGuard],
            },
            {
                path: '',
                outlet: 'sidemenu',
                component: SideMenuComponent
            }
        ]
    },
    {
        path: 'about',
        component: AboutComponent
    },
    {
        path: 'doctors-schedule/:id',
        component: DoctorsScheduleComponent,
        canActivate: [AuthGuard],
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: 'login',
        component: SignInComponent
    },
    {
        path: 'logoff',
        component: SignOutComponent
    },
    {
        path: 'doctors',
        component: DoctorsComponent,
        children: [
            {
                path: '',
                component: DoctorProfilesComponent
            },
            {
                path: ':id',
                component: DoctorProfileComponent
            },
            {
                path: 'department/:departmentId',
                component: DoctorProfilesComponent
            },
            {
                path: '',
                outlet: 'sidemenu',
                component: SideMenuComponent
            }
        ]
    },
    {
        path: 'patients',
        component: PatientsComponent,
        canActivate: [AuthGuard],
        children: [
            {
                path: '',
                component: PatientCardsComponent
            },
            {
                path: 'add',
                component: PatientEditCardComponent
            },
            {
                path: ':id',
                component: PatientProfileComponent
            }
        ]
    },
    {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
    },
    {
        path: '**',
        redirectTo: '/home',
        pathMatch: 'full'
    }
];
