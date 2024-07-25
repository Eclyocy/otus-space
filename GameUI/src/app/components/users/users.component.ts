import { Component } from '@angular/core';
import { User } from '../../models/user';
import { CommonModule } from '@angular/common';

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
  public users: User[] = [];

  public ngOnInit(): void {
    const user: User = {
      id: "1",
      name: "Vasya"
    }

    this.users = [user]

    console.log(this.users);
  }
}
