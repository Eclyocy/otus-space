import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../environments/environment';
import { AuthToken } from '../models/auth.token';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly ACCESS_TOKEN_KEY = 'access_token';
  private readonly REFRESH_TOKEN_KEY = 'refresh_token';
  private readonly EXPIRES_IN_KEY = 'expires_in';

  constructor(
    private http: HttpClient,
    private jwtHelper: JwtHelperService
  ) {}

  login(username: string, password: string): Observable<AuthToken> {
    return this.http
      .post<AuthToken>(
        `${environment.apiUrl}auth/login`,
        { username: username, password: password }
      )
      .pipe(
        tap(response => this.setSession(response))
      );
  }

  getAccessToken(): string | null {
    return localStorage.getItem(this.ACCESS_TOKEN_KEY);
  }

  getRefreshToken(): string | null {
    return localStorage.getItem(this.REFRESH_TOKEN_KEY);
  }

  getUserId(): string | null {
    const accessToken = this.getAccessToken()

    if (accessToken) {
      const decodedToken = this.jwtHelper.decodeToken(accessToken);
      return decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'];
    }

    return null;
  }

  isLoggedIn(): boolean {
    return this.getAccessToken() !== null;
  }

  refreshToken(): Observable<AuthToken> {
    const accessToken = this.getAccessToken();
    const refreshToken = this.getRefreshToken();

    return this.http
      .post<AuthToken>(
      `${environment.apiUrl}auth/refresh`,
      { token: accessToken, refreshToken: refreshToken }
    )
    .pipe(
      tap(response => this.setSession(response))
    );
  }

  logout() {
    localStorage.removeItem(this.ACCESS_TOKEN_KEY);
    localStorage.removeItem(this.REFRESH_TOKEN_KEY);
    localStorage.removeItem(this.EXPIRES_IN_KEY);
  }

  private setSession(authToken: AuthToken) {
    localStorage.setItem(this.ACCESS_TOKEN_KEY, authToken.access_token);
    localStorage.setItem(this.REFRESH_TOKEN_KEY, authToken.refresh_token);
    localStorage.setItem(this.EXPIRES_IN_KEY, authToken.expires_in.toString());
  }
}
