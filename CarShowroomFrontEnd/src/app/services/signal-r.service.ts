import { EventEmitter, Injectable, isDevMode } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { Message } from '../models/message';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;
  private apiDomain: string = "https://localhost:44332"
  private apiChatRoute: string = "/chat";
  signalReceived = new EventEmitter<Message>();

  constructor()
  {
    if(!isDevMode())
      this.apiDomain = "https://carshowroom-app.herokuapp.com";

    this.buildConnection();
    this.startConnection();
  }

  public buildConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.apiDomain + this.apiChatRoute, { accessTokenFactory: () => sessionStorage.getItem('access_token') })
      .build();
  }

  public startConnection = () => {
    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started...');
        this.registerSignalEvents();
      })
      .catch(err => {
        console.log(err)
      });
  }

  public registerSignalEvents() {
    this.hubConnection.on("sendMessage", (data: Message) => {
      this.signalReceived.emit(data);
    })
  }
}
