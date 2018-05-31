import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';


import { AppComponent } from './app.component';
import { RegisterComponent } from './user/register/register.component';
import { UserService } from './shared/user.service';
import { DoctorsService } from './shared/doctors/doctors.service';
import { ImagesService } from './shared/images/images.service';
import { DepartmentsService } from './shared/departments/departments.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HomeComponent } from './home/home.component';
import {routerConfig} from '../router.config';
import { CoursesComponent } from './courses/courses.component';
import { AboutComponent } from './about/about.component';
import { CourseCardsComponent } from './course-cards/course-cards.component';
import { SideMenuComponent } from './categories-menu/categories-menu.component';
import { CoursesCategoryComponent } from './course-category/course-category.component';
import { RouterModule } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { SignInComponent } from './user/sign-in/sign-in.component';
import { UserComponent } from './user/user.component';
import { AuthGuard } from './auth/auth.guard';
import { AuthInterceptor } from './auth/auth.interceptor';
import { SignOutComponent } from './user/sign-out/sign-out.component';

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    HomeComponent,
    CoursesComponent,
    AboutComponent,
    CourseCardsComponent,
    SideMenuComponent,
    CoursesCategoryComponent,
    SignInComponent,
    UserComponent,
    SignOutComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routerConfig)
  ],
  // tslint:disable-next-line:max-line-length
  providers: [UserService, DoctorsService, ImagesService, DepartmentsService, AuthGuard, {provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
