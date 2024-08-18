import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ApiService } from '../../services/api.service';
import { Session } from '../../models/session';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './user.component.html',
  styleUrl: './user.component.css'
})
export class UserComponent {
  private readonly apiService = inject(ApiService)

  public userId: string = "ce011d3f-e4b3-4c79-9151-501782abb080";
  public userSessions: Session[] = [];

  public ngOnInit(): void {
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
