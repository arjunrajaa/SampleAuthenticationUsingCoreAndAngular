import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { User } from '../models/user';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  myAppUrl = '';

  constructor(private http: HttpClient) {
  }


  getUserViewData(): Observable<User[]> {
    return this.http.get<User[]>('/api/user/GetAllUsers').pipe();
  }
}
