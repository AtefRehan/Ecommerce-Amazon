import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  private apiUrl = 'http://localhost:5189/api/Category';

  constructor(private http: HttpClient) { }

  getCategories(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  getsubCategories(categoryId: number): Observable<any[]> {
    const url = `${this.apiUrl}/${categoryId}`;
    return this.http.get<any[]>(url);
  }

  deleteSubcategory(subcategoryId: number): Observable<any> {
    const url = `http://localhost:5189/api/SubCategory/${subcategoryId}`;
    return this.http.delete(url);
  }
  deleteCategory(categoryId: number): Observable<any> {
    const url = `http://localhost:5189/api/Category/${categoryId}`;
    return this.http.delete(url);
  }
}

