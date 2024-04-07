import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ForgotPassword } from 'src/interfaces/forgetpass';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  apiUrl: string = 'http://localhost:5189/api/User/forgot-password'; // Define apiUrl as a class property

  constructor(private http: HttpClient) { }

  forgotPassword(body: ForgotPassword): Observable<any> {
    return this.http.post(this.apiUrl, body); // Use apiUrl directly here
  }

}
