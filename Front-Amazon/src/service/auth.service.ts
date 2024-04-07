import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';
import { Product } from 'src/interfaces/product';
import { UserDetails } from 'src/interfaces/userdetails';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient,private router:Router) { }
  private isAuthenticated = false;

  // emailExists(email: string): Observable<boolean> {
  //   return this.http.get<UserDetails[]>(`${this.apiUrlSignUp}?email=${email}`).pipe(
  //     map(users => users.length > 0),
  //     catchError(error => throwError(error))
  //   );
  // }

  // signUp(user: UserDetails): Observable<any> {
  //   return this.emailExists(user.email ?? '').pipe(
  //     switchMap(emailExists => {
  //       if (emailExists) {
  //         alert('Email already exists');
  //         return throwError('Email already exists');
  //       } else {
  //         return this.http.post(this.apiUrlSignUp, user);
  //       }
  //     }),
  //     catchError(error => throwError(error))
  //   );
  // }

  // signUp(userDetails: UserDetails): Observable<any> {
  //   return this.http.post(this.apiUrlSignUp, userDetails).pipe(
  //     catchError(error => {
  //       let errorMessage = 'An error occurred during sign-up.';
  //       if (error.error && error.error.message) {
  //         errorMessage = error.error.message;
  //       } else if (error.status === 0) {
  //         errorMessage = 'Could not connect to the server. Please try again later.';
  //       }
  //       console.error(errorMessage, error);
  //       return throwError(errorMessage);
  //     })
  //   );
  // }

  logout(): void {
    this.isAuthenticated = false;
    this.router.navigate(['/signin']);
  }

  isAuthenticatedUser(): boolean {
    return this.isAuthenticated;
  }

  forgotPassword(email: string): Observable<any> {
    return this.http.post<any>('http://localhost:5189/api/User/forgot-password', { email });
  }
  private apiUrl = 'http://localhost:5189/api/User/Register';
  registerUser(userData: any): Observable<any> {
    console.log("User Data:", userData);
    return this.http.post(this.apiUrl, userData).pipe(
      catchError((error) => {
        console.error("Error Response:", error);
        throw error; 
      })
    );
  }



  private apiUrls = 'http://localhost:5189/api/User';
  loginUser(credentials: any): Observable<any> {
    return this.http.post(`${this.apiUrls}/login`, credentials);

  }

  deleteSupplier(supplierId: number): Observable<any> {
    const url = `http://localhost:5189/api/Supplier/${supplierId}`;
    return this.http.delete(url);
  }


  signin(_user: UserDetails): Observable<UserDetails> {
    this.isAuthenticated = true;
    return this.http.get<UserDetails[]>
    (
      `http://localhost:5189/api/User/login`
    ).pipe(
      map((response) => {
        if (response.length) {
          return response[0];
        }
        throw new Error('Invalid Email');
      })
    );
  }

  getCurrentUserLocal(): UserDetails {
    let userData = localStorage.getItem('currentUser');
    return userData ? JSON.parse(userData) : null;
  }

  private apiUrlCurrentUserAuth = "http://localhost:5189/api/User/login";

  getCurrentUser(): Observable<any> {
    let token = localStorage.getItem('token');
    if (!token) {
      throw new Error('No authentication token found');
    }

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.get(this.apiUrlCurrentUserAuth, { headers }).pipe(
      map((response: any) => {
        return response.user;
      }),
      catchError(error => {
        console.error('Error fetching current user:', error);
        throw error;
      })
    );
  }





  getallproduct():Observable<Product[]>{
    return this.http.get<Product[]>('https://fakestoreapi.com/products')
  }



  searchProducts(searchText: string): Observable<Product[]> {
    return this.http
      .get<Product[]>('https://fakestoreapi.com/products')
      .pipe(
        map((products) => {
          if (!searchText) {
            return products;
          } else {
            return products.filter((product) => {
              let titleMatch = product.title && product.title.toLowerCase().includes(searchText.toLowerCase());

              let categoryMatch = product.category && product.category.toLowerCase().includes(searchText.toLowerCase());

              return titleMatch||categoryMatch;
            });
          }
        })
      );
  }


}


