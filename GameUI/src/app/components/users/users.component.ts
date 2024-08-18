import { Component, inject } from '@angular/core';
import { User } from '../../models/user';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './users.component.html',
  styleUrls: [
    './users.component.css',
    '../../app.component.css'
  ]
})
export class UsersComponent {
  private readonly apiService = inject(ApiService)

  public users: User[] = [];

  public ngOnInit(): void {
    this.loadUsers();
  }

  public deleteUser(userId: string): void {
    const userConfirmed = confirm(`Are you sure you want to delete user ${userId}?`);

    if (userConfirmed) {
      this.apiService.deleteUser(userId).subscribe({
        next: () => {
          console.log(`User ${userId} deleted.`);
          this.users = this.users.filter(user => user.id !== userId);
        },
        error :(error) => {
          console.error(`Error deleting user ${userId}:`, error);
        }
      });
    }
  }

  private loadUsers(): void {
    this.apiService.getUsers().subscribe({
      next: (users: User[]) => {
        console.log(`Retrieved ${users.length} users.`)
        this.users = users;
      },
      error: (error) => {
        console.error("Error fetching users:", error);
      }
    });
  }
}
