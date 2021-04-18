import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';

//import { PageNotFoundComponent } from './home/page-not-found.component';

const appRoutes: Routes = [

  { path: 'welcome', component: HomeComponent },
  {
    path: 'contacts',
    loadChildren: () =>
      import('./contacts/contact.module').then(m => m.ContactModule)
  },
  { path: '', redirectTo: 'welcome', pathMatch: 'full' },
  //{ path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
