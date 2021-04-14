import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ContactsService } from '../service/contacts.service';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  ngUnsubscribe: Subject<any> = new Subject();
  public forecasts: WeatherForecast[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string, private contactsService: ContactsService) {
    http.get<WeatherForecast[]>(baseUrl + 'weatherforecast').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
    this.contactsService.loadContacts()
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe((result) => console.log(result),
        error => console.error(error));
  }
}

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
