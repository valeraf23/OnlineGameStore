import { Component, Input, OnInit } from '@angular/core';
import { IComment } from "../games/gameModel";

@Component({
  selector: 'comments-popup',
  templateUrl: './comments.popup.component.html',
  styleUrls: ['./comments.popup.component.scss']
})
export class CommentsPopup implements OnInit {

  @Input()
  comment: IComment;

  ngOnInit() {
  }
}
