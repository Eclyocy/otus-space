import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;
  public userUpdated = new Subject<{userId: string, userName: string}>();

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.baseUrl}/user-hub`, {
        withCredentials: false
      })
      .build();

    this.hubConnection.on("RefreshUserName", (userId, userName) => {
      this.userUpdated.next({userId, userName});
    });

    this.hubConnection.start().catch(err => console.error(err));
  }
}
