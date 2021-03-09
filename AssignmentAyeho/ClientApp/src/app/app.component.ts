import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from './services/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']

})
export class AppComponent implements OnInit {

  isLogin = false;

  constructor(private router: Router, private authenticationService: AuthenticationService) {
  }
  ngOnInit(): void {

    // this.authenticationService.currentUser.subscribe(
    //   data => {
    //     if (data && data.isUserAuth) {
    //       this.isLogin = true
    //     }
    //     else {
    //       this.isLogin = false
    //     }
    //   },
    //   error => {
    //     this.isLogin = false;
    //   });
  }
  title = 'פורום';

  logout() {
    this.authenticationService.logout()
    this.authenticationService.isLogin = false;
  }
}




