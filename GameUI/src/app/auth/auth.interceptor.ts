import { HttpInterceptorFn, HttpRequest, HttpHandlerFn, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { catchError, switchMap, Observable, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AuthToken } from '../models/auth.token';

export const authInterceptor: HttpInterceptorFn = (request, next) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (isLoginRequest(request)) {
    return next(request);
  }

  return handleAuthenticatedRequest(request, next, authService, router);
};

function isLoginRequest(request: HttpRequest<any>): boolean {
  return request.url.endsWith('login') && request.method === 'POST';
}

function handleAuthenticatedRequest(
  request: HttpRequest<any>,
  next: HttpHandlerFn,
  authService: AuthService,
  router: Router
): Observable<HttpEvent<any>> {
  const accessToken = authService.getAccessToken();

  if (!accessToken) {
    return handleUnauthorized(authService, router);
  }

  const authenticatedRequest = addAuthorizationHeader(request, accessToken);

  return next(authenticatedRequest).pipe(
    catchError((error: HttpErrorResponse) => handleResponseError(error, request, next, authService, router))
  );
}

function addAuthorizationHeader(request: HttpRequest<any>, token: string): HttpRequest<any> {
  return request.clone({
    setHeaders: {
      'Authorization': `Bearer ${token}`
    }
  });
}

function handleResponseError(
  error: HttpErrorResponse,
  request: HttpRequest<any>,
  next: HttpHandlerFn,
  authService: AuthService,
  router: Router
): Observable<HttpEvent<any>> {
  if (error.status === 401) {
    return authService.refreshToken().pipe(
      switchMap((newToken: AuthToken) => {
        const newRequest = addAuthorizationHeader(request, newToken.access_token);
        return next(newRequest);
      }),
      catchError(() => handleUnauthorized(authService, router))
    );
  }

  return throwError(() => error);
}

function handleUnauthorized(authService: AuthService, router: Router): Observable<never> {
  authService.logout();
  router.navigate(['/login']);
  return throwError(() => new Error('Unauthorized'));
}
