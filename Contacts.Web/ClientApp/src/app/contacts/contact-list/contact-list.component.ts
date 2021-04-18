import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject, Subscription } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

import { Contact } from '../models/contact';
import { ContactService } from '../services/contact.service';

@Component({
  selector: 'contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css']
})
export class ContactListComponent implements OnInit, OnDestroy {
  pageTitle = 'Contacts';
  errorMessage: string;

  displayEmail: boolean;

  contacts: Contact[];

  // Used to highlight the selected contact in the list
  selectedContact: Contact | null;
  sub: Subscription;
  ngUnsubscribe: Subject<any> = new Subject();

  constructor(private contactService: ContactService) { }

  ngOnInit(): void {
    this.sub = this.contactService.selectedContactChanges$.subscribe(
      currentProduct => this.selectedContact = currentProduct
    );
    this.loadContacts();
  }

  loadContacts() {
    this.contactService.loadContacts()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((result: Contact[]) => { this.contacts = result; console.log(this.contacts) },
        error => console.error(error));
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  checkChanged(): void {
    this.displayEmail = !this.displayEmail;
  }

  newContact(): void {
    this.contactService.changeSelectedContact(this.contactService.newContact());
  }

  contactSelected(contact: Contact): void {
    this.contactService.changeSelectedContact(contact);
  }

}
