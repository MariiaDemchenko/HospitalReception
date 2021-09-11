import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { EducationType } from './education-type.model';
import { EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EducationTypesService {
  // authChanged: EventEmitter<any> = new EventEmitter();

  readonly rootUrl = 'http://localhost:55434';
  constructor(private http: HttpClient) { }

  getAllGenders() {
    return this.http.get(this.rootUrl + '/api/lookups/genders');
  }

  getAllEducationTypes() {
    return this.http.get(this.rootUrl + '/api/lookups/educationTypes');
  }

  getAllDisabilityGroups() {
    return this.http.get(this.rootUrl + '/api/lookups/disabilityGroups');
  }

  getAllInformationSources() {
    return this.http.get(this.rootUrl + '/api/lookups/informationSources');
  }

  getAllHabitationMembers() {
    return this.http.get(this.rootUrl + '/api/lookups/habitationMembers');
  }

  getAllPoliclinics() {
    return this.http.get(this.rootUrl + '/api/lookups/policlinics');
  }

  getAllEmploymentTypes() {
    return this.http.get(this.rootUrl + '/api/lookups/employmentTypes');
  }

  getAllLocalityTypes() {
    return this.http.get(this.rootUrl + '/api/lookups/localityTypes');
  }

  getAllRegions() {
    return this.http.get(this.rootUrl + '/api/lookups/regions');
  }
}
