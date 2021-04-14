import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ContactsService {
  private baseServiceUrl = '';
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseServiceUrl = baseUrl;
  }

  loadContacts(): Observable<Object> {
    return this.http.get(`${this.baseServiceUrl}contacts/loadContacts`)
      .pipe(catchError(errorRes => {
        return throwError(errorRes);
      }));
  }
}
