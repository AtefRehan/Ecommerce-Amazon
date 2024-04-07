import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPurchasedItems } from 'src/interfaces/purchasedItems';

@Injectable({
  providedIn: 'root'
})
export class PurchaseditemsService {

  constructor(private http:HttpClient) { }

  private apiUrl = 'https://bison-waders.cyclic.app/purchased';

  getPuchasedItems(email:any):Observable<IPurchasedItems[]>
  {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${localStorage.getItem('token')}` // token is received and stored
    });
    return this.http.post<IPurchasedItems[]>(this.apiUrl,{email},{headers})
  }

}


