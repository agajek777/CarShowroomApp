import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Car } from '../models/car';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  private apiCarRoute: string = "https://localhost:44332/api/car/";
  private apiUserRoute: string = "https://localhost:44332/api/user/";

  constructor(private httpClient: HttpClient) { }

  public getData(pageNum?: string) {
    if (typeof(pageNum) === 'undefined' || pageNum === null || pageNum === '') {
      return this.httpClient.get(this.apiCarRoute, { observe: 'response'});
    }
    return this.httpClient.get(this.apiCarRoute + pageNum, { observe: 'response'});
  }
  public getSingleData(id: string) {
    return this.httpClient.get(this.apiCarRoute + id, { observe: 'response'});
  }

  public editData(body: Car, id: string, jwtToken: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + jwtToken
      })
    };

    return this.httpClient.put(this.apiCarRoute + id, body, httpOptions);
  }

  public addData(body: Car, jwtToken: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + jwtToken
      })
    };

    return this.httpClient.post(this.apiCarRoute, body, httpOptions);
  }

  public deleteData(id: number, jwtToken: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + jwtToken
      })
    };

    console.log(httpOptions);

    return this.httpClient.delete(this.apiCarRoute + id, httpOptions);
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
