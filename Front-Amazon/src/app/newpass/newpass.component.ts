import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-newpass',
  templateUrl: './newpass.component.html',
  styleUrls: ['./newpass.component.css']
})
export class NewpassComponent {
    password!: string;
    confirmPassword!: string;
    myForm: any;

    constructor(private http: HttpClient) {}

    resetPassword(): void {
      let email = localStorage.getItem('resetEmail');
      let token = localStorage.getItem('resetToken');

      if (!email || !token) {
        console.error('Email or token not found in session storage.');
        return;
      }
      if (!this.validatePassword(this.password)) {
        console.error('Password does not meet requirements.');
        return;
      }

      let requestBody = {
        password: this.password,
        confirmPassword: this.confirmPassword,
        email: email,
        token: token
      };

      this.http.post('http://localhost:5189/api/User/reset-password', requestBody)
        .subscribe(
        );
    }
    validatePassword(password: string): boolean {
      let pattern = /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
      return pattern.test(password);
    }
  }
