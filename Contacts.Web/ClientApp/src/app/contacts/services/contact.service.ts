import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Contact } from '../models/contact';
import { Guid } from 'guid-typescript';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  getProducts() {
    throw new Error('Method not implemented.');
  }
  private contactsUrl = 'api/Contacts';
  private contacts: Contact[];
  private baseServiceUrl = '';

  private selectedContactSource = new BehaviorSubject<Contact | null>(null);
  selectedContactChanges$ = this.selectedContactSource.asObservable();

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseServiceUrl = baseUrl;
  }

  loadContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(`${this.baseServiceUrl}contacts/loadContacts`)
      .pipe(tap(data => this.contacts = data), catchError(errorRes => {
        return throwError(errorRes);
      }));
  }

  insertContact(model: Contact) {
    const endpoint = `${this.baseServiceUrl}contacts/insertContact`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(endpoint, model, { headers: headers })
      .pipe(tap(data => {
        this.contacts.push(model);
      }), catchError(errorRes => {
        return throwError(errorRes);
      }));
  }

  updateContact(model: Contact) {
    const endpoint = `${this.baseServiceUrl}contacts/UpdateContact`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put(endpoint, model, { headers: headers })
      .pipe(tap(() => {
        const foundIndex = this.contacts.findIndex(item => item.id === model.id);
        if (foundIndex > -1) {
          this.contacts[foundIndex] = model;
        }
      }), catchError(errorRes => {
        return throwError(errorRes);
      }));
  }

  deleteContact(id: Guid) {
    const endpoint = `${this.baseServiceUrl}contacts/deleteContacts/${id.toString()}`;
    return this.http.delete(endpoint)
      .pipe(tap(() => {
        const foundIndex = this.contacts.findIndex(item => item.id === id);
        if (foundIndex > -1) {
          this.contacts.splice(foundIndex, 1);
        }
      }), catchError(errorRes => {
        return throwError(errorRes);
      }));
  }

  changeSelectedContact(selectedContact: Contact | null): void {
    this.selectedContactSource.next(selectedContact);
  }
  // Return an initialized Contact
  newContact(): Contact {
    return {
      name: '',
      company: 'inc',
      email: 'sara@gmail.com',
      phone: "1234567890"
    };
  }
  private handleError(err: any) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    console.error(err);
    return throwError(errorMessage);
  }

}
