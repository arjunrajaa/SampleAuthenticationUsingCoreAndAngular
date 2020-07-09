import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { User } from '../models/user';
import { Observable } from 'rxjs';
import { UserRole } from '../models/roles';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  myAppUrl = '';

  constructor(private http: HttpClient) {
  }


  getUserViewData(): Observable<User[]> {
    return this.http.get<User[]>('/api/user/GetAllUsersByRole?role=' + UserRole.User).pipe();
  }
  getAdminViewData(): Observable<User[]> {
    return this.http.get<User[]>('/api/user/GetAllUsers').pipe();
  }
}
