import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ChatRoom } from '../shared/entities/Chatroom';
import { ChatUser } from '../shared/entities/Chatuser';
import { Message } from '../shared/entities/Message';

@Component({
  selector: 'app-chatroom',
  templateUrl: './chatroom.component.html',
  styleUrls: ['./chatroom.component.css']
})
export class ChatroomComponent implements OnInit {
  messages: Message[] = [];
  baseUrl: string;
  http: HttpClient;
  roomName: string;
  chatRoom: ChatRoom;
  lastUpdate: string;
  usersOfRoom: ChatUser[];
  user: ChatUser;
  currentMessage: Message;
  messageRefreshInterval: any;
  usersRefreshInterval: any;

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    http: HttpClient, @Inject('BASE_URL') baseUrl: string)
  {
    this.http = http;
    this.baseUrl = baseUrl + "api/ewschat";   
  }

  ngOnInit() {
    if(!localStorage.getItem("user")){
      this.navigateHome();
    }
    this.user = JSON.parse(localStorage.getItem("user"));
    this.route.paramMap.subscribe(params => {
      const roomId = params.get("id");
      this.http.get<ChatRoom>(this.baseUrl + '/rooms/'+roomId).subscribe(result => {
        this.chatRoom = result;        
        this.user.activeRoomId = this.chatRoom.id;
        localStorage.setItem("user", JSON.stringify(this.user));
        this.updateUser(this.user);
        this.setCurrentMessage();
        this.getUsersOfRoom();
        this.setMessageRefresher();
      }, error => console.error(error));     
    });
  }

  setCurrentMessage() {
    this.currentMessage = {
      id: null,
      targetRoomId: this.chatRoom.id,
      createdAtString: null,
      text: "",
      toUserName: null
    }
  }

  navigateHome() {
    this.router.navigate(['home']);
  }

  getMessages(lastUpdate?: string){
    let endpoint = this.baseUrl + '/messages/'+this.chatRoom.id;
    if(lastUpdate){
      endpoint += "/" + lastUpdate;
    }
    console.log(endpoint);
    this.http.get<Message[]>(endpoint).subscribe(result => {
      const resultMessags = result as Message[];     
      if(resultMessags && resultMessags.length > 0){
        this.messages = this.messages.concat(resultMessags);
        this.lastUpdate = this.createCSharpDateTimeNow();
      }      
      //console.log("lastUpdate: ", this.lastUpdate);
    }, error => console.error(error));
  }

  sendMessage(){
    this.currentMessage.createdAtString = this.createCSharpDateTimeNow();
    const messageText = this.currentMessage.text;
    if (messageText.includes("@")){
      const toUser = messageText.substr(0, messageText.indexOf(" "));
      this.currentMessage.toUserName = toUser;
    }
    console.log(this.currentMessage);
    fetch(this.baseUrl + '/messages', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(this.currentMessage),
    })
    .then(response => response.json())
    .catch((error) => {
      console.log(error);
    });
  }

  setMessageRefresher(){
    this.messageRefreshInterval = setInterval(() => {
      this.getMessages(this.lastUpdate);
    }, 500);
  }

  getUsersOfRoom(){
    this.usersRefreshInterval = setInterval(() => {
      this.http.get<ChatUser[]>(this.baseUrl + '/users/room/'+this.chatRoom.id).subscribe(result => {
        this.usersOfRoom = result;
      }, error => console.error(error));
    }, 1000);
    
  }

  createCSharpDateTimeNow() : string{
    var date = new Date();
    var day = date.getDate();       // yields date
    var month = date.getMonth() + 1;    // yields month (add one as '.getMonth()' is zero indexed)
    var year = date.getFullYear();  // yields year
    var hour = date.getHours();     // yields hours 
    var minute = date.getMinutes(); // yields minutes
    var second = date.getSeconds(); // yields seconds

    // After this construct a string with the above results as below
    var time = year + "-" + month + "-" + day + " " + hour + ':' + minute + ':' + second; 
    return time;
  }

  updateUser(user: ChatUser){
    console.log("Chamando update user: ",user);
    fetch(this.baseUrl + '/users', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(this.user),
    })
    .then(response => response.json())
    .catch((error) => {
      console.log(error);
    });
  }

  chatCleanUp(){
    this.lastUpdate = null;
    this.user.activeRoomId = 0;
    this.updateUser(this.user);
    clearInterval(this.messageRefreshInterval);
    clearInterval(this.usersRefreshInterval);
  }

  exitRoom(){
    this.chatCleanUp();
    this.navigateHome();
  }

  ngOnDestroy(){
    this.chatCleanUp();
  }

}
