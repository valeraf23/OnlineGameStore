import { Component, Input } from '@angular/core';
import { IComment } from '../models/IComment';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-comments-popup-selector',
  templateUrl: './comments.popup.component.html',
  styleUrls: ['./comments.popup.component.scss']
})
export class CommentsPopupComponent {
  constructor() { }

  @Input()
  comment: IComment;

  @Input()
  gameId: Guid;

}
