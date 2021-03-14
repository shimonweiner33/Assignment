import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
import { BehaviorSubject } from 'rxjs';
import { ConnectedUsers, ExtendMember } from '../models/user.model';

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
    this.http.get("https://localhost:44353/api/Account/GetAllLogInUsers").subscribe((res: ConnectedUsers) => {
      this._userListResponse$.next(res)

    }, err => {

    })
  }


  updateUserLogIn() {
    this._hubConnecton.on('NewUserLogIn', newUser => {
      const list = this._userListResponse$.getValue();
      let index = -1;
      if (newUser) {
        if (list) {
          index = list.members.map(function (x: ExtendMember) { return x.userName; }).indexOf(newUser.userName)
        }
        if (!list || index == -1) {
          list.members.push(newUser);
        }
        if(index >-1){
          list.members[index] = newUser;
        }
        this._userListResponse$.next(list)
        console.log(newUser);
      }
    });
  }
  updateUserLogOut() {
    this._hubConnecton.on('UserLogOut', logOutUserName => {
      const list = this._userListResponse$.getValue();
      var index = list.members.map(function (x) { return x.userName; }).indexOf(logOutUserName)
      if (index > -1) {
        list.members = list.members.filter(x => x.userName !== logOutUserName);
      }
      this._userListResponse$.next(list)
      console.log(logOutUserName);
    });
  }
}
