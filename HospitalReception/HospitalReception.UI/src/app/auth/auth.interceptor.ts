import { HttpInterceptor, HttpRequest, HttpHandler, HttpUserEvent, HttpEvent, HttpEventType } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { UserService } from '../shared/user.service';
import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { of } from 'rxjs';
import { tap, map } from 'rxjs/operators';
import 'rxjs/add/operator/do';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private router: Router) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (localStorage.getItem('userToken') != null) {
            const clonedreq = req.clone({
                headers: req.headers.set('Authorization', 'Bearer ' + localStorage.getItem('userToken'))
            });
            return next.handle(clonedreq)
                .do(
                    succ => { },
                    err => {
                        if (err.status === 401) {
                            localStorage.removeItem('userToken');
                        }
                    }
                );
        } else {
            return next.handle(req.clone());
        }
    }
}
