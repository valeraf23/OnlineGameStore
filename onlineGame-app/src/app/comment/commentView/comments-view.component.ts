import { Component, Input, OnInit } from '@angular/core';
import { IComment } from "../../games/gameModel";

@Component({
  selector: 'comments',
  templateUrl: './comments-view.component.html',
  styleUrls: ['./comments-view.component.scss']
})
export class CommentsView implements OnInit {

  @Input()
  comments: IComment[];

  isDisplayed: Map<number, boolean>;

  display(i: number) {
    debugger;
    this.isDisplayed[i] = !this.isDisplayed[i];
  }

  ngOnInit(): void {
    this.isDisplayed = new Map<number, boolean>();
    this.comments.forEach((c, i) => this.isDisplayed.set(i, false));
    debugger;
  }

}
