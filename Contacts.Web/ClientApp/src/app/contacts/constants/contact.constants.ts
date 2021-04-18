export class ContactConstants {
  static deleteConfirmation = (value: string) => `Are you sure you want to delete the contact:  ${value}?`;
  static addMessage = "Your contact has been successfully added";
  static updateMessage = "Your contact has been successfully updated";
  static addPageTitle = "Add Contact";
  static updatePageTitle = (value: string) => `Edit Contact: ${value}`;


  static validationMessages = {
    name: {
      required: 'Name is required.',
      minlength: 'Contact name must be at least three characters.',
    },
    phone: {
      required: 'Phone is required.'
    },
    email: {
      required: 'Email is required.',
      email: 'This email does not match email format'
    },
    comapny: {
      required: 'Company is required.'
    }
  };
}
