import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../services/user.service';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';

import { User } from '../models/user';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent {

  userData: string;
  users: User[] = [];
  displayedColumns = ['userName', 'firstName', 'lastName', 'dateOfBirth'];
  dataSource: MatTableDataSource<User>;

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;


  constructor(private userService: UserService) {
    this.loadAllUsers();
    this.dataSource = new MatTableDataSource(this.users);
  }
  
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }
  
 private loadAllUsers() {
    this.userService.getUserViewData().subscribe(result => {
        this.users = result;
      }
    );
  }
}
