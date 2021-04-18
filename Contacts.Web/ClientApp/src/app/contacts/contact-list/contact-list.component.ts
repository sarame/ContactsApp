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

  displayCode: boolean;

  contacts: Contact[];

  // Used to highlight the selected product in the list
  selectedProduct: Contact | null;
  sub: Subscription;
  ngUnsubscribe: Subject<any> = new Subject();

  constructor(private contactService: ContactService) { }

  ngOnInit(): void {
    this.sub = this.contactService.selectedContactChanges$.subscribe(
      currentProduct => this.selectedProduct = currentProduct
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
    this.displayCode = !this.displayCode;
  }

  newProduct(): void {
    this.contactService.changeSelectedContact(this.contactService.newContact());
  }

  contactSelected(contact: Contact): void {
    this.contactService.changeSelectedContact(contact);
    console.log(contact);
  }

}
