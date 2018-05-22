import { Component, OnInit, NgModule } from '@angular/core';
import { NgForm, FormGroupDirective, FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from '../../shared/user.service';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  isLoginError = false;
  myform: FormGroup;
  UserName: FormControl;
  Password: FormControl;
  Errors: string;

  constructor(private userService: UserService, private router: Router) {
  }

  ngOnInit() {
    this.createFormControls();
    this.createForm();
    this.myform.reset();
  }

  createFormControls() {
    this.UserName = new FormControl('', [
      Validators.required
    ]);
    this.Password = new FormControl('', [
      Validators.required,
      Validators.minLength(8)
    ]);
  }

  createForm() {
    this.myform = new FormGroup({
      UserName: this.UserName,
      Password: this.Password
    });
  }

  OnSubmit(form: NgForm) {
    alert('hrrr');
    this.userService.userAuthentication(this.myform.value)
      // tslint:disable-next-line:max-line-length
      .subscribe((data: any) => {
        localStorage.setItem('userToken', data.access_token);
        this.router.navigate(['/home']);
      },
        (err: HttpErrorResponse) => {
          this.isLoginError = true;
          alert(err.message);
          this.myform.setErrors(['Invalid credentials']);
        });
  }
}
