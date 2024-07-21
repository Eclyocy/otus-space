import { Component, inject } from "@angular/core";
import { User } from "@app/models/user";
import { ApiService } from "@app/services/api.service";

@Component({
  selector: 'app-users-component',
  templateUrl: 'users.component.html',
  styleUrls: []
})
export class UsersComponent {
  private readonly apiService = inject(ApiService)

  public users: User[] = [];

  public ngOnInit(): void {
    console.log("Hello");

    this.apiService
      .getUsers()
      .subscribe(x => this.users = x);
  }
}
