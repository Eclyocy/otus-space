import { Routes } from '@angular/router';
import { UsersComponent } from '@app/components/users/users.component';
import { TempComponent } from '@app/components/temp/temp.component';

export const routes: Routes = [
  { path: 'users', component: UsersComponent },
  { path: 'temp', component: TempComponent },
];
