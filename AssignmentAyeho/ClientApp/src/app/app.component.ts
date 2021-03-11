import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ExtendMember } from './models/user.model';
import { AuthenticationService } from './services/authentication.service';
import { HubsService } from './services/hubs.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']

})
export class AppComponent implements OnInit {


  isLogin = false;
  logInUsersList: ExtendMember[] = [];

  constructor(private router: Router, private hubsService: HubsService, private authenticationService: AuthenticationService) {
    if (this.authenticationService.isLogin) {
      this.router.navigate(['/post-list']);
    }
  }
  ngOnInit(): void {

    this.hubsService.userList$.subscribe((members: any) => {
      this.logInUsersList = members ? members.members : [];
      console.log("logInUsersList: ", this.logInUsersList);
    });
  }
  title = 'פורום';

  logout() {
    this.authenticationService.logout()
    this.authenticationService.isLogin = false;
  }
}




