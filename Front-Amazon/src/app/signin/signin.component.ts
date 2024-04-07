import { Token } from '@angular/compiler';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserDetails } from 'src/interfaces/userdetails';
import { AuthService } from 'src/service/auth.service';
import { GlobalstateService } from 'src/service/globalstate.service';


@Component({
  selector: 'app-auth',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css']
})

export class SignInComponent implements OnInit, OnDestroy {
  isLoginExist = localStorage.getItem('LoginExist!');
  Nameuser = localStorage.getItem('NameUser!');
  myForm!: FormGroup;
  errorMessage: string | null = null;
  currentUser: UserDetails | undefined;
  buttonClicked: boolean = false;
  credentials: UserDetails = {
    email: null,
    password: null,
    username: null,
    phoneNumber: null
  };
isTokenExist: any;
responseMessage: string | null = null;


  constructor(private auth: AuthService, private formBuilder: FormBuilder, private router: Router, private globalState: GlobalstateService) {
    this.myForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(8), Validators.pattern('^(?=.*[A-Z])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,}$')]]
    });
  }

  ngOnDestroy(): void {
    this.globalState.updateHeaderSwitch(true);
  }

 ngOnInit() {
    this.myForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8)]]
    });
  }


  login() {
    console.log("Login function called.");
    if (this.myForm.valid) {
      let credentials = this.myForm.value;
      this.auth.loginUser(credentials).subscribe(
        (response: any) => {
          this.responseMessage = response.message || 'Login successful!';
          localStorage.setItem('LoginExist!', response.token);
          localStorage.setItem('NameUser!', response.email);
          localStorage.setItem('IsAdmin!', response.isAdmin);
          localStorage.setItem('cartId', response.cartId.toString());
          localStorage.setItem('userId', response.userId)
          window.location.reload()
        },
        (error) => {
          this.errorMessage = error.message || 'An error occurred during login.';
        }
      );
    }
  }



  onsubmit(): void {
    this.auth.signin(this.myForm.value).subscribe((res) => {
      localStorage.setItem('token', res.username!);
      localStorage.setItem('Idtoken', res.email!);
      this.auth.getCurrentUser().subscribe(res => {
        this.currentUser = res;
        this.globalState.updateCurrentUser(res);
        window.location.reload();
      });
      this.router.navigate(['']);
    }, (error) => {
      if (error.error.code === 'UserNotConfirmedException') {
        this.errorMessage = "Please verify your account first";
      } else if (error.error.code === 'NotAuthorizedException') {
        this.errorMessage = "Please check email/password";
        this.buttonClicked = false;
      }
      console.error('Error occurred while:', error);
    });
  }

  errorGone(): void {
    this.errorMessage = null;
  }

  onContinueClicked(): void {
    this.buttonClicked = true;
    if (this.myForm.valid) {
      this.onsubmit();
    }}
}
