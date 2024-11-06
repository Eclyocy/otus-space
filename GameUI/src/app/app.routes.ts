import { Routes } from '@angular/router';
import { UserComponent } from './components/user/user.component';
import { UsersComponent } from './components/users/users.component';
import { ShipComponent } from './components/ship/ship.component';
import { AuthGuard } from './auth/auth.guard';
import { LoginComponent } from './components/login/login.component';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  {
    path: '',
    canActivate: [AuthGuard],
    children: [
      { path: 'users', component: UsersComponent },
      { path: 'users/:userId', component: UserComponent },
      { path: 'users/:userId/sessions/:sessionId/ships/:shipId', component: ShipComponent }
    ]
  },
  { path: '**', redirectTo: 'users' }
];
