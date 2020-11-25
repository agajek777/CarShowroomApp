import { Injectable, isDevMode } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Car } from '../models/car';
import { send } from 'process';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  private apiDomain: string = "https://localhost:44332"
  private apiCarRoute: string = "/api/car/";
  private apiUserRoute: string = "/api/user/";
  private apiMessageRoute: string = "/api/message/";

  constructor(private httpClient: HttpClient) {
    if (!isDevMode()) {
      this.apiDomain = "https://carshowroom-app.herokuapp.com"
    }
  }

  public getData(pageNum?: string) {
    if (typeof(pageNum) === 'undefined' || pageNum === null || pageNum === '') {
      return this.httpClient.get(this.apiDomain + this.apiCarRoute, { observe: 'response'});
    }
    return this.httpClient.get(this.apiDomain + this.apiCarRoute + pageNum, { observe: 'response'});
  }
  public getSingleData(id: string) {
    return this.httpClient.get(this.apiDomain + this.apiCarRoute + id, { observe: 'response'});
  }

  public getUsers() {
    return this.httpClient.get(this.apiDomain + this.apiUserRoute + "getusers", { observe: 'response'});
  }

  public getMessages(sender: string, receiver: string) {
    return this.httpClient.get(this.apiDomain + this.apiMessageRoute + "?userId1=" + sender + "&userId2=" + receiver, { observe: 'response' });
  }

  public sendMessage(receiver: string, text: string, jwtToken: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + jwtToken,
        'skip': 'true'
      })
    };

    var body = {
      receiverId: receiver,
      text: text
    }

    return this.httpClient.post(this.apiDomain + this.apiMessageRoute, body, httpOptions);
  }

  public editData(body: Car, id: string, jwtToken: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + jwtToken
      })
    };

    return this.httpClient.put(this.apiDomain + this.apiCarRoute + id, body, httpOptions);
  }

  public addData(body: Car, jwtToken: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + jwtToken
      })
    };

    return this.httpClient.post(this.apiDomain + this.apiCarRoute, body, httpOptions);
  }

  public deleteData(id: number, jwtToken: string) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + jwtToken
      })
    };

    console.log(httpOptions);

    return this.httpClient.delete(this.apiDomain + this.apiCarRoute + id, httpOptions);
  }

  public register(username: string, password: string) {
    return this.httpClient.post(this.apiDomain + this.apiUserRoute + 'register',
      {
        userName: username,
        password: password
      }
    )
  }

  public login(username: string, password: string) {
    return this.httpClient.post(this.apiDomain + this.apiUserRoute + 'login',
      {
        userName: username,
        password: password
      }
    )
  }
}
