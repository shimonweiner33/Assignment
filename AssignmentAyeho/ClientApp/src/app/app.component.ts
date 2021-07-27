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

  roomFormGroup: FormGroup;
  roomList: Room[] = [];

  logInUsersList: ExtendMember[] = [];

  constructor(private router: Router, private hubsService: HubsService, private authenticationService: AuthenticationService, private roomsService: RoomsService) {
    if (this.authenticationService.isLogin) {
      let mainRoom = 1;
      this.router.navigate(['/post-list', mainRoom]);
    }
    else {
      this.router.navigate(['/login']);
    }
  }
  ngOnInit(): void {
    this.hubsService.userList$.subscribe((members: any) => {
      if (members) {
        this.logInUsersList = members.members;
        if (this.authenticationService.currentUserValue) {
          let logincurrentUser = this.logInUsersList.find(x => x.userName === this.authenticationService.currentUserValue.member.userName)
          if(logincurrentUser && logincurrentUser.userConnectinonId)
          this.authenticationService.currentUserValue.member.userConnectinonId = logincurrentUser.userConnectinonId;
        }
      }
      else {
        this.logInUsersList = [];
      }
    });
    this.initListFormGroup();
    this.roomsService.updateRoomListAfterChangesByOther();

    this.roomsService.roomList$.subscribe((rooms: any) => {
      this.roomFormGroup.reset();
      this.initListFormGroup();
    });

    if (this.authenticationService.isLogin && !this.authenticationService.currentUserValue) {
      this.authenticationService.updateCurrentUser();
    }
  }
  title = 'Forum';

  logout() {
    this.authenticationService.logout()
    this.authenticationService.isLogin = false;
  }

  //Rooms
  addUserToRoom(user: any) {
    if (this.roomsService.openDialogAddRoom) {
      let users = this.roomFormGroup.value.users;
      if (users && !users.some(x => x.userName === user.userName)) {
        users.push(user)
      }
    }
  }
  addRoom() {
    if (!((this.roomFormGroup.value.users.map(x => x.userName)).includes(this.authenticationService.currentUserValue.member.userName))) {
      this.roomFormGroup.value.users.push(this.authenticationService.currentUserValue.member)
    }
    this.roomsService.AddRoom(this.roomFormGroup.value);
    this.roomsService.openDialogAddRoom = false;
    
  }
  cancelRoom() {
    this.roomFormGroup.reset();
    this.initListFormGroup();
    this.roomsService.openDialogAddRoom = !this.roomsService.openDialogAddRoom
  }
  initListFormGroup() {

    this.roomFormGroup = new FormGroup({
      users: new FormControl([]),
      roomName: new FormControl(''),
      roomNum: new FormControl(0)
    });
  }
}




