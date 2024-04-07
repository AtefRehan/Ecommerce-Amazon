import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserDetails } from 'src/interfaces/userdetails';
import { AuthService } from 'src/service/auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent {
  userData: UserDetails = { username: null, email: null, phoneNumber: null, password: null };
  myForm!: FormGroup;
  responseMessage: string | null = null;
  buttonClicked: any;
  router: any;
  phoneNumber!: string;

  constructor(private fb: FormBuilder, private userService: AuthService) { }

  ngOnInit(): void {
    this.myForm = this.fb.group({
      username: ['', [Validators.required, Validators.minLength(4)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^(012|011|010|015)\d{8}$/)]],
      password: ['', [Validators.required, Validators.pattern(/^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/)]]
    });
  }


  // ngOnInit(): void {
  //   this.myForm = this.fb.group({
  //     username: ['', Validators.required],
  //     email: ['', [Validators.required, Validators.email]],
  //     phoneNumber: ['', Validators.required],
  //     password: ['', [Validators.required, Validators.minLength(8)]]
  //   });
  // }


  registerUser() {
    if (this.myForm.valid) {
      let userData = this.myForm.value;
      this.userService.registerUser(userData).subscribe(
        (response: any) => {
          if (response.success) {
            this.responseMessage = 'Registration successful!';
            this.myForm.reset()
          } else {
            this.responseMessage = response.message || 'Registration successful!';
            // this.phoneNumber=''
          }
        },
        error => {
          this.responseMessage = error.message || 'An error occurred during registration.';
        }
      );
    }
  }




  responseGone() {
    this.responseMessage = null;
  }

}
