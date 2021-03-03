import { Component, OnInit } from '@angular/core';
//import { HubConnection, HubConnectionBuilder } from '@aspnet/signalr';
//import { HubsService } from './services/hubs.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']

})
export class AppComponent implements OnInit{
  //public _hubConnecton: HubConnection;
  constructor() {//private hubsService: HubsService
  }
  ngOnInit(): void {
//     this._hubConnecton = new HubConnectionBuilder().withUrl('https://localhost:44353/message').build();

// this._hubConnecton.on('send', data => {
//     console.log(data);
// });

// this._hubConnecton.start()
// .then(() => console.log("connected"));

          //.then(() => connection.invoke('send', 'Hello'));
        //   this.hubsService._hubConnecton.on('send', data => {
        //     console.log(data);
        // });
  }
  title = 'app';
}
