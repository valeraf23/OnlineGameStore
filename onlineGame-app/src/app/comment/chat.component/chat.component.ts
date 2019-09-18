import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { IComment } from "../../games/gameModel";
import { GameService} from "../../games/game.service";
import { CommentService} from "../../games/comment.service";


@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss'],
})
export class ChatComponent implements OnInit {
  constructor(private gameService: GameService, private commentService: CommentService) {}

  @Input()
  gameId: string;

  @Input()
  commentId: string;

  message = {
    name: "",
    body: ""
  };

  sendMessage() {

    if (this.isCanAddComment())
      return;

    if (this.commentId !== '') {
      this.gameService.addAnswerToComment(this.gameId, this.commentId, JSON.stringify(this.message)).subscribe();
    } else {
      this.gameService.addCommentToGame(this.gameId, JSON.stringify(this.message)).subscribe();
    }

    this.commentService.sendComment();
  }


  private isCanAddComment(): boolean {
    return this.gameId === '' && this.message.body === '' && this.message.name === '';
  }

  ngOnInit() {

  }
}
