import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder} from '@microsoft/signalr';
import { environment } from '../environments/environment';
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShipSignalRService {
  private hubConnection: HubConnection;
  private shipSubject: Subject<string>;

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${environment.shipBaseUrl}/notificationsHub`, {
        withCredentials: false
      })
      .build();

    this.shipSubject = new Subject<string>();

    this.hubConnection.on("Refresh", (message) => {
      console.log("SignalR message received", message);
      this.shipSubject.next(message);
    });

    this.hubConnection.start().catch(err => console.error(err));
  }

  public joinGroup(shipId: string): Observable<string> {
    console.log("Subscribing to SignalR for ship", shipId);
    this.hubConnection.invoke('Subscribe', shipId);
    return this.shipSubject.asObservable();
  }

  public leaveGroup(shipId: string) {
    console.log("Unsubscribing from SignalR for ship", shipId);
    this.hubConnection.invoke('Unsubscribe', shipId);
  }
}
