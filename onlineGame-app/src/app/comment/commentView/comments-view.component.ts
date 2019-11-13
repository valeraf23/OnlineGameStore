import { Component, Input } from '@angular/core';
import { IComment } from 'src/app/models/IComment';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-comments-selector',
  templateUrl: './comments-view.component.html',
  styleUrls: ['./comments-view.component.scss']
})
export class CommentsViewComponent {
  @Input()
  comments: IComment[];

  @Input()
  commentId: Guid;

  @Input()
  gameId: Guid;
}
