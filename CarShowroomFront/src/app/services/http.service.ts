import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Car } from '../models/car';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  private route: string = "https://localhost:44332/api/car/";

  constructor(private httpClient: HttpClient) { }

  public getData(id?: string) {
    if (typeof(id) === 'undefined' || id === null || id === '') {
      return this.httpClient.get(this.route);
    }
    return this.httpClient.get(this.route + id);
  }

  public addData(body: Car) {
    return this.httpClient.post(this.route, body);
  }

  public deleteData(id: number) {
    return this.httpClient.delete(this.route + id);
  }
}
