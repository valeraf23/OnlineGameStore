import { Component, OnInit, Input } from '@angular/core';
import { GameService } from 'src/app/core/game.service';
import { OpenIdConnectService } from 'src/app/core/authentication/open-id-connect.service';
import { CommentsService } from '../comments.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent implements OnInit {
  constructor(
    private commentService: CommentsService,
    private gameService: GameService,
    private openIdConnectService: OpenIdConnectService
  ) {}

  @Input()
  gameId: string;

  @Input()
  commentId: string;

  get UserName(): string {
    return this.openIdConnectService.authContext.userProfile.name;
  }
  message = {
    name: this.UserName,
    body: ''
  };

  sendMessage() {
    if (this.isCanAddComment()) {
      return;
    }
    if (this.commentId) {
      this.gameService
        .addAnswerToComment(
          this.gameId,
          this.commentId,
          JSON.stringify(this.message)
        )
        .subscribe({
          next: () =>
            this.gameService.getComments(this.gameId).subscribe({
              next: cmt => {
                this.commentService.addComments(cmt);
              }
            })
        });
    } else {
      this.gameService
        .addCommentToGame(this.gameId, JSON.stringify(this.message))
        .subscribe({
          next: () =>
            this.gameService.getComments(this.gameId).subscribe({
              next: cmt => {
                this.commentService.addComments(cmt);
              }
            })
        });
    }
  }

  private isCanAddComment(): boolean {
    return this.gameId === '' && this.message.body === '';
  }

  ngOnInit() {}
}
