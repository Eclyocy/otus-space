import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { User } from '../../models/user';
import { Ship } from '../../models/ship';
import { Subscription } from 'rxjs';
import { ShipSignalRService } from '../../services/ship.signalr.service';

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
        this.loadUserSessionShip();
      },
      error: (error) => {
        console.error("Error making move:", error);
      }
    });
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
        console.log(ship);

        if (this.ship)
        {
          this.shipSubscription = this.shipSignalRService.joinGroup(this.ship.id).subscribe(
            (message) => {
              console.log(message);
            }
          );
        }
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
