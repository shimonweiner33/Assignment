import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Room } from '../models/rooms-display.model';
import { AuthenticationService } from '../services/authentication.service';
import { RoomsService } from '../services/rooms.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  roomList: Room[] = [];

  constructor( private router: Router, private authenticationService: AuthenticationService, private roomsService: RoomsService) {
    if (this.authenticationService.isLogin) {
      this.router.navigate(['/post-list', 1]);
    }
    else {
      this.router.navigate(['/login']);
    }
  }
  ngOnInit(): void {
    
    this.roomsService.roomList$.subscribe((rooms: any) => {
      this.roomList = rooms ? rooms.rooms : [];
    });
  }

  logout() {
    this.authenticationService.logout()
    this.authenticationService.isLogin = false;
  }
}
