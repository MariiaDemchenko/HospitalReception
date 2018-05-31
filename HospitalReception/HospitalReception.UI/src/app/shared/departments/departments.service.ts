import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { Department } from './department.model';
import { EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DepartmentsService {
  // authChanged: EventEmitter<any> = new EventEmitter();

  readonly rootUrl = 'http://localhost:55434';
  constructor(private http: HttpClient) { }

  getAllDepartments() {
    return this.http.get(this.rootUrl + '/api/departments');
  }
}
