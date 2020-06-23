import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Car } from '../models/car';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  constructor(private httpService: HttpClient) { }

  public getData(route: string) {
    return this.httpService.get(route);
  }

  public addData(route: string, body: Car) {
    return this.httpService.post(route, body);
  }
}
