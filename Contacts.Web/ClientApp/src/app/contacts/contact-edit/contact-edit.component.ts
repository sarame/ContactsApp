import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Subject, Subscription } from 'rxjs';

import { Contact } from '../models/contact';
import { ContactService } from '../services/contact.service';
import { GenericValidator } from '../../shared/generic-validator';
import { NumberValidators } from '../../shared/number.validator';
import { takeUntil } from 'rxjs/operators';
import { ContactConstants } from '../constants/contact.constants';

@Component({
  selector: 'contact-edit',
  templateUrl: './contact-edit.component.html'
})
export class ContactEditComponent {
  pageTitle = 'Contact Edit';
  errorMessage = '';
  contactForm: FormGroup;

  contact: Contact | null;
  sub: Subscription;

  // Use with the generic validation message class
  displayMessage: { [key: string]: string } = {};
  private validationMessages: { [key: string]: { [key: string]: string } };
  private genericValidator: GenericValidator;
  ngUnsubscribe: Subject<any> = new Subject();

  constructor(private fb: FormBuilder, private contactService: ContactService) {

    // Defines all of the validation messages for the form.
    this.validationMessages = ContactConstants.validationMessages;
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    // Define the form group
    this.contactForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      phone: ['', Validators.required],
      company: ['', Validators.required],
      email: ['',[ Validators.required, Validators.email]]
    });

    // Watch for changes to the currently selected product
    this.sub = this.contactService.selectedContactChanges$.subscribe(
      currentProduct => this.displayContact(currentProduct)
    );

    // Watch for value changes for validation
    this.contactForm.valueChanges.subscribe(
      () => this.displayMessage = this.genericValidator.processMessages(this.contactForm)
    );
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
    this.ngUnsubscribe.unsubscribe();
  }

  blur(): void {
    this.displayMessage = this.genericValidator.processMessages(this.contactForm);
  }

  displayContact(contact: Contact | null): void {
    // Set the local product property
    this.contact = contact;

    if (contact) {
      // Reset the form back to pristine
      this.contactForm.reset();

      // Display the appropriate page title
      if (!contact.id) {
        this.pageTitle = ContactConstants.addPageTitle;
      } else {
        this.pageTitle = ContactConstants.updatePageTitle(contact.name);
      }

      // Update the data on the form
      this.contactForm.patchValue({
        name: contact.name,
        phone: contact.phone,
        company: contact.company,
        email: contact.email
      });
    }
  }

  cancelEdit(product: Contact): void {
    this.displayContact(product);
  }

  deleteContact(contact: Contact): void {
    if (contact && contact.id) {
      if (confirm(ContactConstants.deleteConfirmation(contact.name))) {
        this.contactService.deleteContact(contact.id).subscribe(() => { this.contactService.changeSelectedContact(null) },
          error => console.error(error));
      }
    } else {
      this.contactService.changeSelectedContact(null);
    }
  }

  saveProduct(originalContact: Contact): void {
    if (this.contactForm.valid) {
      if (this.contactForm.dirty) {
        const contact = { ...originalContact, ...this.contactForm.value };
        if (!contact.id) {
          this.contactService.insertContact(contact).pipe(takeUntil(this.ngUnsubscribe))
            .subscribe((result: Contact) => {
              this.contactService.changeSelectedContact(contact);
              alert(ContactConstants.addMessage);
            },
              error => console.error(error));
        } else {
          this.contactService.updateContact(contact).pipe(takeUntil(this.ngUnsubscribe))
            .subscribe((result: Contact) => {
              this.contactService.changeSelectedContact(contact);
              alert(ContactConstants.updateMessage);
            },
              error => console.error(error));
        }
      }
    }
  }

}
