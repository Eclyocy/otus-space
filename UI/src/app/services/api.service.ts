import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { User } from '@app/models/user';
import { environment } from '@environments/environment';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root'})
export class ApiService {
  constructor(
    private readonly http: HttpClient
  ) {}

  public getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${environment.apiUrl}/users`);
  }
}
