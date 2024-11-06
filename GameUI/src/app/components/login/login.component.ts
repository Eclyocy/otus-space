// login.component.ts
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.errorMessage = ''; // Clear any previous error messages
    this.authService.login(this.username, this.password).subscribe({
      next: () => {
        // Login successful
        this.router.navigate(['/users']); // Redirect to the users page or any default page after login
      },
      error: (error) => {
        // Login failed
        console.error('Login error:', error);
        this.errorMessage = 'Invalid username or password. Please try again.';
      }
    });
  }
}
