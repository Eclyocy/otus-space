import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder} from '@microsoft/signalr';
import { environment } from '../environments/environment';
import { BehaviorSubject, Observable } from 'rxjs'; // Adjust the import path as needed
import { Ship } from '../models/ship';

@Injectable({
  providedIn: 'root'
})
export class ShipSignalRService {
  private hubConnection: HubConnection;
  private shipSubject: BehaviorSubject<Ship | null>;

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${environment.shipBaseUrl}/notificationsHub`, {
        withCredentials: false
      })
      .build();

    this.shipSubject = new BehaviorSubject<Ship | null>(null);

    this.hubConnection.on("Refresh", (message: any) => {
      console.log("SignalR message received", message);

      try {
        if (this.isValidShip(message)) {
          this.shipSubject.next(message);
        } else {
          throw new Error("Invalid ship data");
        }
      } catch (error) {
        console.error("Error parsing SignalR message:", error);

        this.shipSubject.next(null);
      }
    });

    this.hubConnection.start().catch(err => console.error(err));
  }

  private isValidShip(ship: any): ship is Ship {
    return (
      ship &&
      typeof ship.id === "string" &&
      typeof ship.day === "number" &&
      Array.isArray(ship.resources)
    );
  }

  public joinGroup(shipId: string): Observable<Ship | null> {
    console.log("Subscribing to SignalR for ship", shipId);
    this.hubConnection.invoke('Subscribe', shipId);
    return this.shipSubject.asObservable();
  }

  public leaveGroup(shipId: string) {
    console.log("Unsubscribing from SignalR for ship", shipId);
    this.hubConnection.invoke('Unsubscribe', shipId);
  }

  public getCurrentShip(): Ship | null {
    return this.shipSubject.getValue();
  }
}
