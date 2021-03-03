import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})
export class HubsService {
  public _hubConnecton: HubConnection;

 

  constructor() {
    this._hubConnecton = new HubConnectionBuilder().withUrl('https://localhost:44353/message').build();

this._hubConnecton.start()
.then(() => console.log("connected"));
  }

}
