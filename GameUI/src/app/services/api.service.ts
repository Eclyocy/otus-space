import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../models/user';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { Session } from '../models/session';
import { Ship } from '../models/ship';

@Injectable({ providedIn: 'root'})
export class ApiService {

  constructor(private http: HttpClient) { }

  public createUser(userName: string, password: string) {
    return this.http.post<User>(
      `${environment.apiUrl}users`,
      { name: userName, password: password });
  }

  public getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${environment.apiUrl}users`);
  }

  public getUser(userId: string): Observable<User> {
    return this.http.get<User>(`${environment.apiUrl}users/${userId}`);
  }

  public deleteUser(userId: string): Observable<any> {
    return this.http.delete(`${environment.apiUrl}users/${userId}`);
  }

  public createUserSession(userId: string): Observable<Session> {
    return this.http.post<Session>(
      `${environment.apiUrl}users/${userId}/sessions`,
      { });
  }

  public getUserSessions(userId: string): Observable<Session[]> {
    return this.http.get<Session[]>(`${environment.apiUrl}users/${userId}/sessions`);
  }

  public deleteUserSession(userId: string, sessionId: string): Observable<any> {
    return this.http.delete(`${environment.apiUrl}users/${userId}/sessions/${sessionId}`);
  }

  public getUserSessionShip(userId: string, sessionId: string): Observable<Ship> {
    return this.http.get<Ship>(`${environment.apiUrl}users/${userId}/sessions/${sessionId}/ship`);
  }

  public postUserSessionMakeMove(userId: string, sessionId: string): Observable<any> {
    return this.http.post(
      `${environment.apiUrl}users/${userId}/sessions/${sessionId}/make-move`,
      { });
  }
}
