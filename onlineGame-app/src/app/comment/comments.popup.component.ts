import { Component, Input, OnInit } from '@angular/core';
import { IComment, System } from "../games/gameModel";
import Guid = System.Guid;
import { CommentService } from '../games/comment.service';

@Component({
  selector: 'comments-popup',
  templateUrl: './comments.popup.component.html',
  styleUrls: ['./comments.popup.component.scss']
})
export class CommentsPopup implements OnInit {
  constructor(private commentService: CommentService) { }

  @Input()
  comment: IComment;

  @Input()
  gameId: Guid;

  setComment() {
    this.commentService.setComment(this.comment);
  }

  ngOnInit() {
  }
}
