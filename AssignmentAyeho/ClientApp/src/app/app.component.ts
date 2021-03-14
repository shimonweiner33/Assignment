import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Room } from './models/rooms-display.model';
import { ExtendMember } from './models/user.model';
import { AuthenticationService } from './services/authentication.service';
import { HubsService } from './services/hubs.service';
import { RoomsService } from './services/rooms.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']

})
export class AppComponent implements OnInit {

  openDialogAdd = false;
  roomFormGroup: FormGroup;
  roomList: Room[] = [];

  isLogin = false;
  logInUsersList: ExtendMember[] = [];

  constructor(private router: Router, private hubsService: HubsService, private authenticationService: AuthenticationService, private roomsService: RoomsService) {
    if (this.authenticationService.isLogin) {
      this.router.navigate(['/post-list']);
    }
  }
  ngOnInit(): void {
    this.hubsService.userList$.subscribe((members: any) => {
      this.logInUsersList = members ? members.members : [];
      console.log("logInUsersList: ", this.logInUsersList);
    });
    this.initListFormGroup();
    this.roomsService.updateRoomListAfterChangesByOther();
  }
  title = 'פורום';

  logout() {
    this.authenticationService.logout()
    this.authenticationService.isLogin = false;
  }

  addRoom() {
    this.roomsService.AddRoom(this.roomFormGroup.value);
    this.openDialogAdd = false;
    this.roomFormGroup.reset();
  }
  initListFormGroup() {
    this.roomFormGroup = new FormGroup({
      users: new FormControl(''),
      roomName: new FormControl(''),
      roomNum: new FormControl(1)
    });
  }
}




