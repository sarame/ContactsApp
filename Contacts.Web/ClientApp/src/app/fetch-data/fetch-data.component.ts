import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ContactsService } from '../services/contacts.service';
import { ContactModel } from '../models/contact.model';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  ngUnsubscribe: Subject<any> = new Subject();
  contacts: ContactModel[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private contactsService: ContactsService) {
    this.loadContacts();

  }

  loadContacts() {
    this.contactsService.loadContacts()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((result: ContactModel[]) => this.contacts = result,
        error => console.error(error));
  }

  onSubmit() {
    const model: ContactModel = {
      name: "rrr",
      phone: "989656"
    };
    this.contactsService.insertContact(model)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((result) => {
        alert("success!")
      },
        error => console.error(error));
  }
}

