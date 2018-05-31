import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from './shared/user.service';
import { NgModule } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  userClaims: any;
  title = 'mmm';

  constructor(private router: Router, private userService: UserService) {
    this.userService.authChanged.subscribe((someData?: any) => {
      if (someData) {
        this.userService.getUserClaims().subscribe((data: any) => {
          this.userClaims = data;
        });
      }
    });
  }

  ngOnInit() {
    this.userService.authChanged.subscribe((someData?: any) => {
      this.userClaims = someData;
    });
  }

  Logout() {
    localStorage.removeItem('userToken');
    this.router.navigate(['/login']);
    this.userService.authChanged.emit(null);
  }
}
