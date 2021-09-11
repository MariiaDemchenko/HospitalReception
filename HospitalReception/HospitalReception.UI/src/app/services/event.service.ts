import { Injectable } from '@angular/core';
import { Event } from '../models/event';
import { Http } from '@angular/http';
import { ExtractData, HandleError } from './service-helper';

@Injectable()
export class EventService {
  readonly rootUrl = 'http://localhost:55434';

  constructor(private http: Http) { }

  get(doctorId: number): Promise<Event[]> {
    // alert('GET APPOINTMENTS');
    return this.http.get(this.rootUrl + '/api/doctors/appointments/' + doctorId)
      .toPromise()
      .then(ExtractData)
      .catch(HandleError);
  }

  insert(event: Event): Promise<Event> {
    return this.http.post(this.rootUrl + '/api/doctors/appointments/add', event)
      .toPromise()
      .then(ExtractData)
      .catch(HandleError);
  }

  update(event: Event): Promise<void> {
    return this.http.put(this.rootUrl + '/api/doctors/appointments/edit', event)
      .toPromise()
      .then(ExtractData)
      .catch(HandleError);
  }

  remove(id: number): Promise<void> {
    return this.http.delete(this.rootUrl + '/api/doctors/appointments/' + id)
      .toPromise()
      .then(ExtractData)
      .catch(HandleError);
  }

  getConsultationHours(id: number) {
    // tslint:disable-next-line:prefer-const
    return this.http.get(this.rootUrl + '/api/doctors/consultationHours/' + id);
  }
}

// {id: 1, start_date: "2017-09-01 00:00", end_date: "2017-09-01 13:00", text: "Event 1"},
// {id: 2, start_date: "2017-09-03 00:00", end_date: "2017-09-03 13:00", text: "Event 2"},
