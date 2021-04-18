import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { ContactShellComponent } from './contact-shell/contact-shell.component';
import { ContactListComponent } from './contact-list/contact-list.component';
import { ContactEditComponent } from './contact-edit/contact-edit.component';

const productRoutes: Routes = [
  { path: '', component: ContactShellComponent }
];

@NgModule({
  imports: [
    SharedModule,
    RouterModule.forChild(productRoutes)
  ],
  declarations: [
    ContactShellComponent,
    ContactListComponent,
    ContactEditComponent
  ]
})
export class ContactModule { }
