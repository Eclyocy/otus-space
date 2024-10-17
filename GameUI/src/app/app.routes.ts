import { Routes } from '@angular/router';
import { UserComponent } from './components/user/user.component';
import { UsersComponent } from './components/users/users.component';
import { ShipComponent } from './components/ship/ship.component';

export const routes: Routes = [
  { path: '', redirectTo: 'users', pathMatch: 'full' },
  { path: 'users', component: UsersComponent },
  { path: 'users/:userId', component: UserComponent },
  { path: 'users/:userId/sessions/:sessionId/ships/:shipId', component: ShipComponent }
];
