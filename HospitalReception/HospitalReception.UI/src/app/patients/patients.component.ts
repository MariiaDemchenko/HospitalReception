import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { PatientsService } from '../shared/patients/patients.service';

@Component({
  selector: 'app-patients',
  templateUrl: './patients.component.html',
  styleUrls: ['./patients.component.css']
})
export class PatientsComponent implements OnInit {

  constructor(private router: Router, private route: ActivatedRoute, private doctorsService: PatientsService) { }

  ngOnInit() {
  }
}
