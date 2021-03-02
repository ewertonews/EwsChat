import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ChatRoom } from '../shared/entities/Chatroom';
import { ChatUser } from '../shared/entities/Chatuser';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public chatRooms: ChatRoom[];
  nickName: string;
  canEnterRoom: boolean = false;
  user: ChatUser = { userId: null, nickName: null, activeRoomId: 0};
  baseUrl: string;
  message: string;

  ngOnInit(){
    const userData = localStorage.getItem("user");
    if(userData){
      this.user = JSON.parse(userData);
      this.message = "All set, " + this.user.nickName+"!";
      this.canEnterRoom = true;
    }
  }

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl + "api/ewschat";
    http.get<ChatRoom[]>(this.baseUrl + '/rooms').subscribe(result => {
      this.chatRooms = result;
    }, error => console.error(error));
  }

  setUser(){
    this.user.nickName = this.nickName;
    fetch(this.baseUrl + '/users', {
      method: 'POST', // or 'PUT'
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(this.user),
    })
    .then(response => response.json())
    .then(data => {
      this.user = data;
      this.canEnterRoom = true;
      this.nickName = null;
      localStorage.setItem("user", JSON.stringify(data));
      this.message = "All set, " + this.user.nickName+"!";
      console.log('Success:', data);
    })
    .catch(() => {
      this.message = "Ops! Someone have already picked this one :/ \nChoose another one.";
    });
  }
}



