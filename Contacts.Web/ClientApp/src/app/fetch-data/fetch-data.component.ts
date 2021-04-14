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
  public forecasts: WeatherForecast[];
  model: ContactModel;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private contactsService: ContactsService) {
    http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
    const g: ContactModel = {
      name: "tt",
      phone: 989656
    };
    this.contactsService.insertContact(g)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((result) => {
        console.log(result);
        this.contactsService.loadContacts()
          .pipe(takeUntil(this.ngUnsubscribe))
          .subscribe((result) => console.log(result),
            error => console.error(error));
      },
        error => console.error(error));
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
