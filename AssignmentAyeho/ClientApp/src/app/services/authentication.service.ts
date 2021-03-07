import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../models/user.model';


@Injectable({ providedIn: 'root' })
export class AuthenticationService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  constructor(private http: HttpClient) {
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
        this.currentUserSubject.next(data.body);
      }, err => {

      })
    // fetch('https://localhost:44353/api/Account/Login', {
    //   method: 'POST',
    //   headers: {
    //     Accept: 'application/json',
    //     'Content-Type': 'application/json',
    //   },
    //   credentials: 'same-origin',
    //   body: JSON.stringify({
    //     username: username,
    //     password: password,
    //   }),
    // }).then(res => {
    //   // console.log(res.headers.get('set-cookie')); // undefined
    //   // console.log(document.cookie); // nope
    //   return res.json();
    // }).then(json => {
    //   if (json.success) {
    //     //this.setState({ error: '' });
    //     //this.context.router.push(json.redirect);
    //   }
    //   else {
    //     //this.setState({ error: json.error });
    //   }
    // });
  }

  logout() {
    // remove user from local storage to log user out
    //localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }
}