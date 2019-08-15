import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IComment } from "../../games/gameModel";


@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
})
export class ChatComponent implements OnInit {
  constructor(private http: HttpClient) { }
  @Output() onSendMessage: EventEmitter<IComment> = new EventEmitter();

  message = {
    name: '',
    body: '',
  };

  sendMessage() {
    if (this.message.body !== '' && this.message.name !== '') {
      
    }
  }
  ngOnInit() {

  }
}
