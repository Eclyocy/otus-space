import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { User } from '../../models/user';
import { Ship } from '../../models/ship';
import { Subscription } from 'rxjs';
import { ShipSignalRService } from '../../services/ship.signalr.service';
import { ShipResource } from '../../models/ship.resource';
import { getShipDisplayName, getSortedResources } from '../../utils/ship.utils';
import { getShipResourceDisplayName } from '../../utils/ship.resource.utils';

@Component({
  selector: 'app-ship',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './ship.component.html',
  styleUrls: [
    './ship.component.css',
    '../../app.component.css'
  ]
})
export class ShipComponent {
  private readonly apiService = inject(ApiService)
  private shipSubscription?: Subscription
  private _userId: string
  private _sessionId: string

  public userName: string = "";
  public ship: Ship | undefined;
  public shipResources: ShipResource[] | undefined;

  public get userId(): string {
    return this._userId;
  }

  public get sessionId(): string {
    return this._sessionId;
  }

  public constructor(
    private route: ActivatedRoute,
    private router: Router,
    private shipSignalRService: ShipSignalRService
  ) {
    this._userId = this.fetchValueFromRoute('userId');
    this._sessionId = this.fetchValueFromRoute('sessionId');
  }

  public ngOnInit(): void {
    this.loadUser();
    this.loadUserSessionShip();
  }

  public ngOnDestroy(): void {
    if (this.shipSubscription) {
      this.shipSubscription.unsubscribe();
    }

    if (this.ship)
    {
      this.shipSignalRService.leaveGroup(this.ship.id);
    }
  }

  public makeMove(): void {
    this.apiService.postUserSessionMakeMove(this.userId, this.sessionId).subscribe({
      next: () => {
        console.log("Move made.");
      },
      error: (error) => {
        console.error("Error making move:", error);
      }
    });
  }

  public getShipDisplayName(): string {
    return getShipDisplayName(this.ship);
  }

  public getSortedShipResources(): ShipResource[] {
    return getSortedResources(this.ship);
  }

  public getResourceName(resource: ShipResource): string {
    return getShipResourceDisplayName(resource);
  }

  private fetchValueFromRoute(paramName: string): string {
    const value = this.route.snapshot.paramMap.get(paramName);

    if (value === null) {
      throw new Error(`${paramName} is required`);
    }

    return value;
  }

  private loadUser(): void {
    this.apiService.getUser(this.userId).subscribe({
      next: (user: User) => {
        this.userName = user.name;
      },
      error: (error) => {
        console.error("Error fetching user:", error);
      }
    });
  }

  private loadUserSessionShip(): void {
    this.apiService.getUserSessionShip(this.userId, this.sessionId).subscribe({
      next: (ship: Ship) => {
        this.ship = ship;
        console.log("Loaded user session ship:", ship);
        this.setupShipSubscription(this.ship.id);
      },
      error: (error) => {
        if (error.status === 404) {
          this.handleShipNotFound();
        } else {
          console.error(`Error fetching user ${this.userId} session ${this.sessionId} ship:`, error);
        }
      }
    });
  }

  private setupShipSubscription(shipId: string)
  {
    this.shipSubscription = this.shipSignalRService.joinGroup(shipId).subscribe(
      (ship: Ship | null) => {
        if (ship) {
          console.log("Received ship update:", ship);

          if (this.ship && this.ship.state !== ship.state) {
            if (ship.state === "Crashed") {
              alert("Mission Failed!\nSpace ship crashed.");
            } else if (ship.state === "Arrived") {
              alert("Mission Complete!\nSpace ship arrived.");
            }
          }

          this.ship = ship;
        } else {
          console.log("Received an invalid ship update, unable to process.");
        }
      }
    );
  }

  private handleShipNotFound(): void {
    const userConfirmed = confirm('Ship not found.\Do you want to delete this orphan session?');

    if (userConfirmed) {
      this.apiService.deleteUserSession(this.userId, this.sessionId).subscribe({
        next: () => {
          console.log("Session deleted.");
          this.router.navigate(['/users', this.userId]);
        },
        error: (error) => {
          console.error("Error while deleting the session:", error);
        }
      });
    }
  }
}
