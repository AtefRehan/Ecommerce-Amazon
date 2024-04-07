import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent {
  users: any[] = [];
  adminUsernames: string[] = []

  constructor(private http: HttpClient) { }

  getAllUsers(): void {
    let apiUrl = 'http://localhost:5189/api/User/Accounts';

    this.http.get<any[]>(apiUrl).subscribe(
      (users) => {
        console.log('Users:', users);
        this.users = users;
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );

  }

  addAdmin(applicationUserId: string, index: number): void {
    if (!this.adminUsernames[index]) {
      alert('Please enter admin username.');
      return;
    }

    const adminData = {
      username: this.adminUsernames[index]
    };

    this.http.post(`http://localhost:5189/api/Role/AddAdmin?userId=${applicationUserId}`, adminData)
      .subscribe(
        (data) => {
          console.log('Admin added successfully:', data);
        },
        (error) => {
          console.error('Error adding admin:', error);
        }
      );
  }

}
