import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Register } from '../../models/user.model';
import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private authenticationService: AuthenticationService,
    private router: Router) {
    //redirect to home if already logged in
    if (this.authenticationService.isLogin) {
      this.router.navigate(['/post-list',1]);
    }

  }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      firstName: ['', Validators.compose([Validators.required,Validators.maxLength(30)])],
      lastName: ['', Validators.compose([Validators.required,Validators.maxLength(30)])],
      verificationPassword: ['', Validators.required],
      phoneArea: ['',Validators.maxLength(3)],
      phoneNumber: ['',Validators.maxLength(7)],

    });
    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';


      this.authenticationService.currentUser.subscribe(
        data => {
          if (this.authenticationService.isLogin) {
            this.router.navigate(['/post-list',1]);
          }
        },
        error => {
          this.loading = false;
        });
  }
  get f() { return this.registerForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid || this.f.password.value !== this.f.verificationPassword.value) {
      return;
    }

    this.loading = true;
    const member: Register = new Register();
    member.verificationPassword = this.f.verificationPassword.value;
    member.firstName = this.f.firstName.value;
    member.lastName = this.f.lastName.value;
    member.password = this.f.password.value;
    member.phoneArea = this.f.phoneArea.value;
    member.phoneNumber = this.f.phoneNumber.value;
    member.userName = this.f.username.value;


    this.authenticationService.register(member)
    //this.router.navigate([this.returnUrl]);
  }
}
