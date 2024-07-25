import { Component, inject } from '@angular/core';
import { User } from '../../models/user';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';

@Component({
  selector: 'app-users',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './users.component.html',
  styleUrl: './users.component.css'
})
export class UsersComponent {
  private readonly apiService = inject(ApiService)

  public users: User[] = [];

  public ngOnInit(): void {
    /*
    const user: User = {
      id: "1",
      name: "Vasya"
    }

    this.users = [user]

    console.log(this.users);
    */
    this.apiService.getUsers().subscribe({
      next: (users: User[]) => {
        this.users = users;
      },
      error: (error) => {
        console.error("Error fetching users.");
      }
    });

    console.log(this.users);
  }
}
