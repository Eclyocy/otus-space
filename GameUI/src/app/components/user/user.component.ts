import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { Session } from '../../models/session';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { User } from '../../models/user';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './user.component.html',
  styleUrls: [
    './user.component.css',
    '../../app.component.css'
  ]
})
export class UserComponent {
  private readonly apiService = inject(ApiService)
  private _userId: string

  public userName: string = "";
  public userSessions: Session[] = [];

  public get userId(): string {
    return this._userId;
  }

  public constructor(
    private route: ActivatedRoute,
  ) {
    const id = this.route.snapshot.paramMap.get('userId');
    if (id === null) {
      throw new Error('UserId is required');
    }
    this._userId = id;
  }

  public ngOnInit(): void {
    console.log(this.userId);

    this.apiService.getUser(this.userId).subscribe({
      next: (user: User) => {
        this.userName = user.name;
      }
    })

    this.apiService.getUserSessions(this.userId).subscribe({
      next: (userSessions: Session[]) => {
        this.userSessions = userSessions;
      },
      error: (error) => {
        console.error("Error fetching user sessions.");
      }
    });

    console.log(this.userSessions);
  }
}
