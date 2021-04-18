import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Subject, Subscription } from 'rxjs';

import { Contact } from '../models/contact';
import { ContactService } from '../services/contact.service';
import { GenericValidator } from '../../shared/generic-validator';
import { NumberValidators } from '../../shared/number.validator';
import { takeUntil } from 'rxjs/operators';

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
    // These could instead be retrieved from a file or database.
    this.validationMessages = {
      name: {
        required: 'Name is required.'
      },
      phone: {
        required: 'Phone code is required.'
      }
    };

    // Define an instance of the validator for use with this form,
    // passing in this form's set of validation messages.
    this.genericValidator = new GenericValidator(this.validationMessages);
  }

  ngOnInit(): void {
    // Define the form group
    this.contactForm = this.fb.group({
      name: ['', Validators.required],
      phone: ['', Validators.required],
      company: ['', Validators.required],
      email: ['', Validators.required]
    });

    // Watch for changes to the currently selected product
    this.sub = this.contactService.selectedContactChanges$.subscribe(
      currentProduct => { this.displayProduct(currentProduct); console.log(currentProduct) }
    );

    // Watch for value changes for validation
    this.contactForm.valueChanges.subscribe(
      () => this.displayMessage = this.genericValidator.processMessages(this.contactForm)
    );
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  // Also validate on blur
  // Helpful if the user tabs through required fields
  blur(): void {
    this.displayMessage = this.genericValidator.processMessages(this.contactForm);
  }

  displayProduct(contact: Contact | null): void {
    // Set the local product property
    this.contact = contact;

    if (contact) {
      // Reset the form back to pristine
      this.contactForm.reset();

      // Display the appropriate page title
      if (!contact.id) {
        this.pageTitle = 'Add Contact';
      } else {
        this.pageTitle = `Edit Contact: ${contact.name}`;
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
    // Redisplay the currently selected product
    // replacing any edits made
    this.displayProduct(product);
  }

  deleteProduct(contact: Contact): void {
    //if (contact && contact.id) {
    //  if (confirm(`Really delete the product: ${contact.name}?`)) {
    //    this.contactService.deleteProduct(product.id).subscribe({
    //      next: () => this.contactService.changeSelectedProduct(null),
    //      error: err => this.errorMessage = err
    //    });
    //  }
    //} else {
    //  // No need to delete, it was never saved
    //  this.contactService.changeSelectedProduct(null);
    //}
  }

  saveProduct(originalProduct: Contact): void {
    if (this.contactForm.valid) {
      if (this.contactForm.dirty) {
        // Copy over all of the original product properties
        // Then copy over the values from the form
        // This ensures values not on the form, such as the Id, are retained
        const contact = { ...originalProduct, ...this.contactForm.value };

        if (!contact.id) {
          this.contactService.insertContact(contact).pipe(takeUntil(this.ngUnsubscribe))
            .subscribe((result: Contact) => {
              this.contactService.changeSelectedContact(result)
              alert("Your contact has been successfully added")
            },
              error => console.error(error));
        } else {
          this.contactService.updateContact(contact).pipe(takeUntil(this.ngUnsubscribe))
            .subscribe((result: Contact) => {
              this.contactService.changeSelectedContact(result)
              alert("Your contact has been successfully saved")
            },
              error => console.error(error));
        }
      }
    }
  }

}
