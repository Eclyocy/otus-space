import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from '../models/user';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root'})
export class ApiService {

  constructor(private http: HttpClient) {
    // This service can now make HTTP requests via `this.http`.
  }

  public getUsers(): Observable<User[]> {
    let headers = new HttpHeaders({
      "Access-Control-Allow-Origin": "*"
    });
    let headers_node = { headers: headers }

    return this.http.get<User[]>(`${environment.apiUrl}users`, headers_node);
  }
}
