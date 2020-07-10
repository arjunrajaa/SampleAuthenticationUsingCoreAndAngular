import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
import { AuthService } from '../services/auth.service';
import { UserRole } from '../models/roles';


@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})

export class RegisterUserComponent implements OnInit {

  registerForm: FormGroup;
  submitted = false;
  minDate = '1921-01-01';
  userRole: any;

  constructor(private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router, ) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      role: [],
      isAdmin: [''],
      firstName: ['', [Validators.required, Validators.maxLength(100)]],
      lastName: ['', Validators.maxLength(100)],
      userName: ['', [Validators.required, Validators.email]],
      dateOfBirth: ['', [Validators.required]],
      address: ['', Validators.maxLength(300)],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  // convenience getter for easy access to form fields
  get f() { return this.registerForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }

    this.registerForm.value['role'] = this.registerForm.value['isAdmin'] ? UserRole.Admin : UserRole.User;

    const returnUrl =  '/user-list';
    this.authService.register(this.registerForm.value)
      .pipe(first())
      .subscribe(
        () => {
          this.router.navigate([returnUrl]);
        },
        () => {
          this.registerForm.reset();
          this.registerForm.setErrors({
          });
        });
  }

  onReset() {
    this.submitted = false;
    this.registerForm.reset();
  }

  get roleEnum() {
    return UserRole;
  }
}
