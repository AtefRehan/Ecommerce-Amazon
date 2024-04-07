import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Observer } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReversegeoencodingService {

  constructor(private http: HttpClient) {
  }

  getCurrentLocation(): Observable<GeolocationCoordinates> {
    return new Observable((observer: Observer<GeolocationCoordinates>) => {
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
          (position: GeolocationPosition) => {
            observer.next(position.coords);
            observer.complete();
          },
          (error: GeolocationPositionError) => {
            observer.error(error);
          }
        );
      } else {
        observer.error('Soory,Your Geolocation is not supported by this browser.');
      }
    });
  }

  getReverseLocation(latitude: number, longitude: number): Observable<any> {
    return this.http.get(`https://api.bigdatacloud.net/data/reverse-geocode-client?latitude=${latitude}&longitude=${longitude}&localityLanguage=en`)
  }
}

