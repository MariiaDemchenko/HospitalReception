import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Patient } from './patient.model';
import { Event } from '../../models/event';

@Injectable({
  providedIn: 'root'
})
export class PatientsService {
  readonly rootUrl = 'http://localhost:55434';
  readonly mlUrl = 'http://localhost:4449';
  constructor(private http: HttpClient) { }

  getAllPatients() {
    return this.http.get(this.rootUrl + '/api/patients');
  }

  getPatientById(id: number) {
    return this.http.get(this.rootUrl + '/api/patients/' + id);
  }

  editPatient(patient: Patient) {
    alert(JSON.stringify(patient));
    return this.http.post(this.rootUrl + '/api/patients', patient);
  }

  addPatient(patient: Patient) {
    return this.http.post(this.rootUrl + '/api/patients/add', patient);
  }

  predictRisk(patient: Patient) {
    return this.http.get(this.mlUrl);
  }

  getConsultationHours(id: number) {
    // tslint:disable-next-line:prefer-const
    return this.http.get(this.rootUrl + '/api/doctors/consultationHours/' + id);
  }

  getClosestConsultation(id: number, duration: number) {
    // tslint:disable-next-line:prefer-const
    return this.http.get(this.rootUrl + '/api/doctors/consultationHours/' + id + '/closest/' + duration);
  }

  getAllAppointments(doctorId: number) {
    return this.http.get(this.rootUrl + '/api/doctors/appointments/' + doctorId);
  }

  insertAppointment(event: Event) {
    return this.http.post(this.rootUrl + '/api/doctors/appointments/add', event);
  }

  removeAppointment(id: number) {
    return this.http.delete(this.rootUrl + '/api/doctors/appointments/' + id);
  }

  updateAppointment(event: Event) {
    return this.http.put(this.rootUrl + '/api/doctors/appointments/edit', event);
  }

  getCardRecords(id: number) {
    return this.http.get(this.rootUrl + '/api/patients/appointments/' + id);
  }
}
