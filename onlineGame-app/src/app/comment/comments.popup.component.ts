import { Component, Input, OnInit } from '@angular/core';
import { CommentService } from './comment.service';
import { IComment } from '../models/IComment';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-comments-popup-selector',
  templateUrl: './comments.popup.component.html',
  styleUrls: ['./comments.popup.component.scss']
})
export class CommentsPopupComponent implements OnInit {
  constructor(private commentService: CommentService) {}

  @Input()
  comment: IComment;

  @Input()
  gameId: Guid;

  setComment() {
    this.commentService.setComment(this.comment);
  }

  ngOnInit() {}
}
