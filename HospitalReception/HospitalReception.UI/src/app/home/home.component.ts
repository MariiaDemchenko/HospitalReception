import { Component, OnInit } from '@angular/core';
import { Router, RouterModule, ActivatedRoute } from '@angular/router';
import { UserService } from '../shared/user.service';
import { DoctorsService } from '../shared/doctors/doctors.service';
import { NgModule } from '@angular/core';
import { ImagesService } from '../shared/images/images.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  userClaims: any;
  doctors: any;
  // pager object
  pager: any = {};

  // paged items
  pagedItems: any[];

  // tslint:disable-next-line:max-line-length
  constructor(private router: Router, private userService: UserService, private doctorsService: DoctorsService, private imagesService: ImagesService) {
  }

  getImage(imageId: string) {
    return this.imagesService.getImage(imageId).subscribe((data: any) => {
      return data;
    });
  }

  ngOnInit() {
    this.userService.getUserClaims().subscribe((data: any) => {
      this.userService.authChanged.emit(data);
    });
    this.doctors = this.doctorsService.getAllDoctors().subscribe((data: any) => {
      this.doctors = data;
    });
  }
}
