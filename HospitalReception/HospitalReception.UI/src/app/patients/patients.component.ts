import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { PatientsService } from '../shared/patients/patients.service';
import { UserService } from '../shared/user.service';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})
export class PatientsComponent implements OnInit {
  // tslint:disable-next-line:max-line-length
  constructor(private router: Router, private route: ActivatedRoute, private doctorsService: PatientsService, private userService: UserService) { }
  ngOnInit() {
    this.userService.getUserClaims().subscribe((data: any) => {
      this.userService.authChanged.emit(data);
    });
  }
}
