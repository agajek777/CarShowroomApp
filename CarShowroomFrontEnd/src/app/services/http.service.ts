import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Car } from '../models/car';
import { send } from 'process';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  private apiCarRoute: string = "https://localhost:44332/api/car/";
  private apiUserRoute: string = "https://localhost:44332/api/user/";
  private apiMessageRoute: string = "https://localhost:44332/api/message/";

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

  public getUsers() {
    return this.httpClient.get(this.apiUserRoute + "getusers", { observe: 'response'});
  }

  public getMessages(sender: string, receiver: string) {
    return this.httpClient.get(this.apiMessageRoute + "?userId1=" + sender + "&userId2=" + receiver, { observe: 'response' });
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

    return this.httpClient.post(this.apiMessageRoute, body, httpOptions);
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
