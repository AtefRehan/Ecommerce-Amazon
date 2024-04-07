import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-forgetpass',
  templateUrl: './forgetpass.component.html',
  styleUrls: ['./forgetpass.component.css']
})
export class ForgetpassComponent {
  myForm: FormGroup;
  errorMessage: string = '';

  constructor(private formBuilder: FormBuilder, private http: HttpClient) {
    this.myForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  onSubmit() {
    if (this.myForm.valid) {
      let email = this.myForm.value.email;
      this.http.post<any>(`http://localhost:5189/api/User/forgot-password?email=${email}`, {email} ).subscribe(
        response => {
          console.log('Password reset email sent successfully:', response);
          sessionStorage.setItem('resetEmail', response.email);
          sessionStorage.setItem('resetToken', response.token);
        },
        error => {
          console.error('Error sending password reset email:', error);
          this.errorMessage = 'Error sending password reset email. Please try again later.';
        }
      );
    }
  }
}
