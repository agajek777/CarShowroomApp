import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Car } from '../models/car';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  private apiCarRoute: string = "https://localhost:44332/api/car/";
  private apiUserRoute: string = "https://localhost:44332/api/user/";

  constructor(private httpClient: HttpClient) { }

  public getData(id?: string) {
    if (typeof(id) === 'undefined' || id === null || id === '') {
      return this.httpClient.get(this.apiCarRoute);
    }
    return this.httpClient.get(this.apiCarRoute + id);
  }

  public addData(body: Car) {
    return this.httpClient.post(this.apiCarRoute, body);
  }

  public deleteData(id: number) {
    return this.httpClient.delete(this.apiCarRoute + id);
  }

  public register(username: string, password: string) {
    return this.httpClient.post(this.apiUserRoute + 'register',
      {
        userName: username,
        password: password
      }
    )
  }

  public login(username: string, password: string) {
    return this.httpClient.post(this.apiUserRoute + 'login',
      {
        userName: username,
        password: password
      }
    )
  }
}
