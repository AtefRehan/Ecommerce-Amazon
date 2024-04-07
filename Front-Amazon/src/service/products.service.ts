import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Product } from 'src/interfaces/product';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  private apiUrl = "http://localhost:5189/api/Products";

  httpHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
  });
  constructor(private http: HttpClient) { }

  getProductData(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl)
      .pipe(
        catchError(this.handleError)
      );
  }


  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`)
      .pipe(
        catchError(this.handleError)
      );
  }


  addProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product, { headers: this.httpHeaders })
      .pipe(
        catchError(this.handleError)
      );
  }


 createProduct(productData: Product): Observable<Product> {
    let apiUrl = 'http://localhost:5189/api/Products';
    return this.http.post<Product>(apiUrl, productData);
  }


  deleteProduct(id: number): Observable<void> {
    let url = `${this.apiUrl}/${id}`;
    return this.http.delete<void>(url, { headers: this.httpHeaders })
      .pipe(
        catchError(this.handleError)
      );
  }

  updateProduct(productId: string, product: Product): Observable<Product> {
    let apiUrl = 'http://localhost:5189/api/Products';
    return this.http.put<Product>(`${apiUrl}/${productId}`, product);
  }


  getProductDatabycategory(category: string): Observable<Product[]> {
    let url = this.apiUrl;
    if (category) {
      url += `?category=${category}`;
    }
    return this.http.get<Product[]>(url)
      .pipe(
        catchError(this.handleError)
      );
  }

  getProductDatabySupplierId(supplierId: number): Observable<Product[]> {
    return this.http.get<Product[]>(`http://localhost:5189/api/Supplier/${supplierId}`)
      .pipe(
        catchError(this.handleError)
      );
  }


  getItemDetails(id: any): Observable<any> {
    let url = `${this.apiUrl}/${id}`;
    return this.http.get<any>(url)
      .pipe(
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    return throwError('Something bad happened; please try again later.');
  }
}

