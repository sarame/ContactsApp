import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { ContactModel } from '../models/contact.model';


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

  insertContact(model: ContactModel) {
    const endpoint = `${this.baseServiceUrl}contacts/insertContact`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(endpoint, model, { headers: headers })
      .pipe(catchError(errorRes => {
        return throwError(errorRes);
      }));
  }
}
