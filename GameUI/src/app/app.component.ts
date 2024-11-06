import { Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { UserComponent } from './components/user/user.component';
import { UsersComponent } from './components/users/users.component';
import { LoginComponent } from './components/login/login.component';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    UserComponent,
    UsersComponent,
    LoginComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = 'GameUI';

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.redirectBasedOnAuth();
  }

  private redirectBasedOnAuth() {
    if (this.authService.isLoggedIn()) {
      const userId = this.authService.getUserId();
      if (userId) {
        this.router.navigate(['/users', userId]);
      } else {
        this.router.navigate(['/users']);
      }
    } else {
      this.router.navigate(['/login']);
    }
  }
}
