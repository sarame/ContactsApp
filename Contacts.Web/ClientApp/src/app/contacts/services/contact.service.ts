import { Inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { Observable, of, BehaviorSubject, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';

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
  private Contacts: Contact[];
  private baseServiceUrl = '';

  private selectedContactSource = new BehaviorSubject<Contact | null>(null);
  selectedContactChanges$ = this.selectedContactSource.asObservable();

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseServiceUrl = baseUrl;
  }

  loadContacts(): Observable<Contact[]>  {
    return this.http.get<Contact[]>(`${this.baseServiceUrl}contacts/loadContacts`)
      .pipe(catchError(errorRes => {
        return throwError(errorRes);
      }));
  }

  insertContact(model: Contact){
    const endpoint = `${this.baseServiceUrl}contacts/insertContact`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(endpoint, model, { headers: headers })
      .pipe(catchError(errorRes => {
        return throwError(errorRes);
      }));
  }

  updateContact(model: Contact) {
    const endpoint = `${this.baseServiceUrl}contacts/UpdateContact`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post(endpoint, model, { headers: headers })
      .pipe(catchError(errorRes => {
        return throwError(errorRes);
      }));
  }

  changeSelectedContact(selectedContact: Contact | null): void {
    this.selectedContactSource.next(selectedContact);
  }

  getContacts(): Observable<Contact[]> {
    if (this.Contacts) {
      return of(this.Contacts);
    }
    return this.http.get<Contact[]>(this.contactsUrl)
      .pipe(
        tap(data => console.log(JSON.stringify(data))),
        tap(data => this.Contacts = data),
        catchError(this.handleError)
      );
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

  createContact(Contact: Contact): Observable<Contact> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    // Contact Id must be null for the Web API to assign an Id
    const newContact = { ...Contact, id: null };
    return this.http.post<Contact>(this.contactsUrl, newContact, { headers })
      .pipe(
        tap(data => console.log('createContact: ' + JSON.stringify(data))),
        tap(data => {
          this.Contacts.push(data);
        }),
        catchError(this.handleError)
      );
  }

  deleteContact(id: Guid): Observable<{}> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    const url = `${this.contactsUrl}/${id}`;
    return this.http.delete<Contact>(url, { headers })
      .pipe(
        tap(data => console.log('deleteContact: ' + id)),
        tap(data => {
          const foundIndex = this.Contacts.findIndex(item => item.id === id);
          if (foundIndex > -1) {
            this.Contacts.splice(foundIndex, 1);
          }
        }),
        catchError(this.handleError)
      );
  }

  //updateContact(Contact: Contact): Observable<Contact> {
  //  const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  //  const url = `${this.contactsUrl}/${Contact.id}`;
  //  return this.http.put<Contact>(url, Contact, { headers })
  //    .pipe(
  //      tap(() => console.log('updateContact: ' + Contact.id)),
  //      // Update the item in the list
  //      // This is required because the selected Contact that was edited
  //      // was a copy of the item from the array.
  //      tap(() => {
  //        const foundIndex = this.Contacts.findIndex(item => item.id === Contact.id);
  //        if (foundIndex > -1) {
  //          this.Contacts[foundIndex] = Contact;
  //        }
  //      }),
  //      // Return the Contact on an update
  //      map(() => Contact),
  //      catchError(this.handleError)
  //    );
  //}

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
