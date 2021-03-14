import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Register, User } from '../models/user.model';
import { HubsService } from './hubs.service';
@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;
  public isLogin = this.isCookieExist();

  constructor(private http: HttpClient, private hubsService: HubsService) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(username: string, password: string) {
    return this.http.post<any>(`https://localhost:44353/api/Account/Login`, { username, password },
      { observe: 'response', withCredentials: true })
      .subscribe((data: any) => {
        if (data) {
          if (data.body.isUserAuth) {
            //
            this.hubsService._hubConnecton.invoke("UpdateConnectionId",username);
            //
            this.isLogin = true;
          }
          this.currentUserSubject.next(data.body);
        }
      }, err => {

      })
  }
  register(registerDetails: Register) {

    return this.http.post<any>(`https://localhost:44353/api/Account/Register`, registerDetails,
      { observe: 'response', withCredentials: true })
      .subscribe((data: any) => {
        if (data) {
          if (data.body && data.body.isUserAuth) {
            this.hubsService._hubConnecton.invoke("UpdateConnectionId",registerDetails.userName);

            this.isLogin = true;
          }
          this.currentUserSubject.next(data.body);
        }
      }, err => {

      })
  }

  logout() {

    return this.http.post("https://localhost:44353/api/Account/Logout", null).subscribe((removedUserName: any) => {
      if (removedUserName) {
        this.hubsService._hubConnecton.invoke("RemoveConnectionId",removedUserName.toString());

        this.isLogin = false;
      }
      this.currentUserSubject.next(null);
    }, err => {

    })
  }


  // remove user from local storage to log user out
  //localStorage.removeItem('currentUser');
  isCookieExist() {
    var myCookie = this.getCookie("AppCookie");

    if (myCookie === null) {
      return false
    }
    else {
      return true;
    }
  }
  getCookie(name) {
    var dc = document.cookie;
    var prefix = name + "=";
    var begin = dc.indexOf("; " + prefix);
    if (begin === -1) {
      begin = dc.indexOf(prefix);
      if (begin != 0) return null;
    }
    else {
      begin += 2;
      var end = document.cookie.indexOf(";", begin);
      if (end === -1) {
        end = dc.length;
      }
    }
    // because unescape has been deprecated, replaced with decodeURI
    //return unescape(dc.substring(begin + prefix.length, end));
    return decodeURI(dc.substring(begin + prefix.length, end));
  }
}
