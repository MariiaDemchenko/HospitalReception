import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DoctorsService } from '../shared/doctors/doctors.service';
import { ImagesService } from '../shared/images/images.service';
import { NgModule } from '@angular/core';
import 'rxjs/add/operator/map';
import { PagerService } from '../shared/pager/pager.service';
import { UserService } from '../shared/user.service';

@Component({
  selector: 'app-doctor-profiles',
  templateUrl: './doctor-profiles.component.html',
  styleUrls: ['./doctor-profiles.component.css']
})

export class DoctorProfilesComponent implements OnInit {
  doctors: any;
  departmentId: any;
  private allItems: any[];
  userClaims: any;

  pager: any = {};

  // paged items
  pagedItems: any[];

  // tslint:disable-next-line:max-line-length
  constructor(private router: Router, private route: ActivatedRoute, private doctorsService: DoctorsService,
    private imagesService: ImagesService, private pagerService: PagerService, private userService: UserService) {
  }
  navigate(path) {
    this.router.navigate(['/home', path]);
  }

  addDoctor() {
    this.router.navigate(['/home', 'add']);
  }

  setPage(page: number) {
    this.pager = this.pagerService.getPager(this.allItems.length, page);

    this.pagedItems = this.allItems.slice(this.pager.startIndex, this.pager.endIndex + 1);
  }

  ngOnInit() {
    this.userService.getUserClaims().subscribe((data: any) => {
      this.userService.authChanged.emit(data);
      this.userClaims = data;
    });

    this.route.params.subscribe(params => {
      this.departmentId = params['departmentId'];
      if (this.departmentId) {
        this.doctorsService.getDoctorsByDepartmentId(this.departmentId).
          subscribe((data: any) => {
            this.doctors = data;
          });
        this.doctorsService.getDoctorsByDepartmentId(this.departmentId)
          .subscribe((data: any) => {
            this.allItems = data;
            this.setPage(1);
          });
      } else {
        this.doctorsService.getAllDoctors().
          subscribe((data: any) => {
            this.doctors = data;
          });
        this.doctorsService.getAllDoctors()
          .subscribe((data: any) => {
            this.allItems = data;
            this.setPage(1);
          });
      }
    });
  }
}
