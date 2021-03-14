import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Room, Rooms } from '../models/rooms-display.model';
import { HubsService } from './hubs.service';

@Injectable({
  providedIn: 'root'
})
export class RoomsService {
  private _roomListResponse$ = new BehaviorSubject<Rooms>(null);
  public roomList$ = this._roomListResponse$.asObservable();
  constructor(private http: HttpClient, private hubsService: HubsService) { }
  AddRoom(room: Room) {
    this.http.post("https://localhost:44353/Assignment/CreateOrUpdateRoom", room).subscribe((res: Boolean) => {
      // const list = this._roomListResponse$.getValue();
      // list.posts.push(post);
      // this._roomListResponse$.next(list)
      console.log("add seccessfuly");
    }, err => {

    })
  }

  DeleteRoom(roomNum: number) {

    this.http.post("https://localhost:44353/Assignment/DeleteRoom", roomNum).subscribe((res: Boolean) => {

    }, err => {

    })
  }
  updateRoomListAfterChangesByOther() {
    this.hubsService._hubConnecton.on('CreateOrUpdateRoom', room => {
      const list = this._roomListResponse$.getValue();
      var index = list.rooms.map(function (x) { return x.roomNum; }).indexOf(room.roomNum);
      if (index > -1) {
        list.rooms[index] = room;
      }
      else {
        list.rooms.push(room);
      }
      this._roomListResponse$.next(list)
      console.log(room);
    });
  }
}
