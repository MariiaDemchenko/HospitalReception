import { Component, OnInit, NgModule } from '@angular/core';
import { NgForm, FormGroupDirective, FormGroup, FormControl, Validators } from '@angular/forms';
import { UserService } from '../../shared/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  toastr: any;
  myform: FormGroup;
  Email: FormControl;
  Password: FormControl;
  UserName: FormControl;
  Errors: string;

  constructor(private userService: UserService, private router: Router) {
  }

  ngOnInit() {
    this.createFormControls();
    this.createForm();
    this.myform.reset();
  }

  createFormControls() {
    this.Email = new FormControl('', [
      Validators.required,
      Validators.pattern('[^ @]*@[^ @]*')
    ]);
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
      Email: this.Email,
      Password: this.Password,
      UserName: this.UserName
    });
  }

  OnSubmit() {
    // this.userService.registerUser(this.myform.value)
    this.userService.registerUser(this.myform.value)
      .subscribe((data: any) => {
        if (data.Succeeded) { this.router.navigate(['/login']); } else { this.myform.setErrors(data.Errors); }
      });
  }
}
