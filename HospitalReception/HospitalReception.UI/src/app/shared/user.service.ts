import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Response} from '@angular/http';
import {Observable} from 'rxjs';
import 'rxjs/add/operator/map';
import { User } from './user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
readonly rootUrl = 'http://localhost:55434';
  constructor(private http: HttpClient) { }

  registerUser(user: User) {
    alert(user);
    alert(user.Email);
    alert(user.Password);
    const body: User = {
      UserName : user.UserName,
      Password : user.Password,
      Email: user.Email,
      FirstName: user.FirstName,
      LastName: user.LastName
    };

    return this.http.post(this.rootUrl + '/api/users/register', body);
  }

  userAuthentication(user: User) {
    // tslint:disable-next-line:prefer-const
    let data = 'username=' + user.UserName + '&email=' + user.Email + '&password=' + user.Password + '&grant_type=password';
    const reqHeader = new HttpHeaders({ 'Content-Type': 'application/x-www-urlencoded', 'No-Auth': 'True' });
    return this.http.post(this.rootUrl + '/token', data, { headers: reqHeader });
  }

  getUserClaims() {
    alert(this.rootUrl + '/api/users/getUserClaims');
    return  this.http.get(this.rootUrl + '/api/users/getUserClaims');
   }
}
