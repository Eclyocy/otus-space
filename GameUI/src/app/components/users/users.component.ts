import { Component, inject } from '@angular/core';
import { User, CreateUser } from '../../models/user';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    FormsModule
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
  public newUser: CreateUser = { name: "", password: "" };

  public ngOnInit(): void {
    this.loadUsers();
  }

  createUser() {
    if (!this.newUser.name || !this.newUser.password) {
      alert("All fields are required.");
      return;
    }

    console.log('Creating user:', this.newUser);

    this.apiService.createUser(this.newUser.name, this.newUser.password).subscribe({
      next: (user: User) => {
        console.log(`Retrieved ${user.name} user.`);

        this.users.push(user);
        this.newUser = { name: "", password: "" };
      },
      error: (error) => {
        console.error(`Error creating user ${this.newUser.name}:`, error);
      }
    });
  }

  public deleteUser(userId: string): void {
    const userConfirmed = confirm(`Are you sure you want to delete user ${userId}?`);

    if (userConfirmed) {
      this.apiService.deleteUser(userId).subscribe({
        next: () => {
          console.log(`User ${userId} deleted.`);
          this.users = this.users.filter(user => user.id !== userId);
        },
        error: (error) => {
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
