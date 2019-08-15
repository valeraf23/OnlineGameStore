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
  isFormDisplayed: Map<number, boolean>;

  display(i: number) {
    debugger;
    this.isDisplayed[i] = !this.isDisplayed[i];
  }
  displayForm(i: number) {
    debugger;
    this.isFormDisplayed[i] = !this.isFormDisplayed[i];
  }
  ngOnInit(): void {
    this.isDisplayed = new Map<number, boolean>();
    this.comments.forEach((c, i) => this.isDisplayed.set(i, false));
    this.isFormDisplayed = new Map<number, boolean>();
    this.comments.forEach((c, i) => this.isFormDisplayed.set(i, false));
    debugger;
  }

}
