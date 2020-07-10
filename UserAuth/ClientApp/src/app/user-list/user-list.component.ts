import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../services/user.service';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { User } from '../models/user';
import { DatePipe } from '@angular/common';
import { UserRole } from '../models/roles';
import { AuthService } from '../services/auth.service';
import * as moment from 'moment';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {

  users: User[] = [];
  userDataSubscription: any;
  userData = new User();
  userRole = UserRole;
  dataSource = new MatTableDataSource();
  displayedColumns = ['userName', 'firstName', 'lastName', 'age', 'address'];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  
  constructor(private userService: UserService, private authService: AuthService) {
  }

  ngOnInit() {

    this.getUer();
    this.loadData();
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  private loadData() {
    if (this.userData.role == UserRole[UserRole.Admin]) {
      this.loadAllUsers();
    }
    else {
      this.loadAllUserData();
    }
  }

 private loadAllUserData() {
    this.userService.getUserViewData().subscribe(result => {
      this.users = result;
      this.setData();
      }
    );
 }

  private loadAllUsers() {
    this.userService.getAdminViewData().subscribe(result => {
      this.users = result;
      this.setData()
      }
    );
  }

  private setData() {
    this.dataSource = new MatTableDataSource(this.users);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.calculateAge();
  }

  private getUer() {
    this.userDataSubscription = this.authService.userData.subscribe(data => {
      this.userData = data;

    });
  }

  public ageFromDateOfBirthday(dateOfBirth: any): number {
    return moment().diff(dateOfBirth, 'years');
  }

  private calculateAge() {
    this.users.forEach(obj => {
      obj.age = this.ageFromDateOfBirthday(obj.dateOfBirth);
    })
  }
}
