import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Patient } from './patient.model';

@Injectable({
  providedIn: 'root'
})
export class PatientsService {
  readonly rootUrl = 'http://localhost:55434';
  constructor(private http: HttpClient) { }

  getAllPatients() {
    return this.http.get(this.rootUrl + '/api/patients');
  }

  getPatientById(id: number) {
    return this.http.get(this.rootUrl + '/api/patients/' + id);
  }

  editPatient(patient: Patient) {
    return this.http.post(this.rootUrl + '/api/patients', patient);
  }

  addPatient(patient: Patient) {
    return this.http.post(this.rootUrl + '/api/patients/add', patient);
  }

  getConsultationHours(id: number) {
    // tslint:disable-next-line:prefer-const
    return this.http.get(this.rootUrl + '/api/doctors/consultationHours/' + id);
  }
}
