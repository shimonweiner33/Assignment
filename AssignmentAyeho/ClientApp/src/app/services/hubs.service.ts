import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { BehaviorSubject } from 'rxjs';
import { ConnectedUsers } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class HubsService {
  public _hubConnecton: HubConnection;
  private _userListResponse$ = new BehaviorSubject<ConnectedUsers>(null);
  public userList$ = this._userListResponse$.asObservable();


  constructor(private http: HttpClient) {
    this._hubConnecton = new HubConnectionBuilder().withUrl('https://localhost:44353/message').build();
    this.GetUserList();

    this._hubConnecton.start()
      .then(() => console.log("connected"));
  }


  GetUserList() {
    //this._userListResponse$.next(null)
    this.http.get("https://localhost:44353/api/Account/GeAllLogInUsers").subscribe((res: ConnectedUsers) => {
      this._userListResponse$.next(res)

    }, err => {

    })
  }


  updateUserLogIn() {
    this._hubConnecton.on('NewUserLogIn', newUserName => {
      const list = this._userListResponse$.getValue();
      if (newUserName && !list || list.members.indexOf(newUserName) == -1) {
        list.members.push(newUserName);
        this._userListResponse$.next(list)
        console.log(newUserName);
      }
    });
  }
  updateUserLogOut() {
    this._hubConnecton.on('UserLogOut', logOutUserName => {
      const list = this._userListResponse$.getValue();
      var index = list.members.indexOf(logOutUserName);
      if (index > -1) {
        list.members = list.members.filter(userName => userName !== logOutUserName);
      }
      this._userListResponse$.next(list)
      console.log(logOutUserName);
    });
  }
}
