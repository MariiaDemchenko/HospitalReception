import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImagesService {
  readonly rootUrl = 'http://localhost:55434';
  constructor(private http: HttpClient) { }

  getImage(imageId: string) {
    return this.http.get(this.rootUrl + '/api/images/' + imageId);
  }
}
