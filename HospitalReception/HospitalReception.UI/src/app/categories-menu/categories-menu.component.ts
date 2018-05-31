import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DepartmentsService } from '../shared/departments/departments.service';

@Component({
  selector: 'app-categories-menu',
  templateUrl: './categories-menu.component.html',
  styleUrls: ['./categories-menu.component.css']
})

export class SideMenuComponent implements OnInit {
  departments: any;

  constructor(route: ActivatedRoute, private departmentsService: DepartmentsService) {
    route.params.subscribe(params => console.log('side menu id parameter', params['id']));
  }
  ngOnInit() {
    this.departmentsService.getAllDepartments().subscribe((data: any) => {
      this.departments = data;
    });
  }
}
