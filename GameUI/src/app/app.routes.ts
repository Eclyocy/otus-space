import { Routes } from '@angular/router';
import { UserComponent } from './components/user/user.component';
import { UsersComponent } from './components/users/users.component';

export const routes: Routes = [
  { path: 'users', component: UsersComponent },
  { path: 'users/:userId', component: UserComponent }
];
