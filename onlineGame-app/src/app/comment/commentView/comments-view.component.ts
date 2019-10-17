import { Component, Input, OnInit } from '@angular/core';
import { IComment } from 'src/app/models/IComment';
import { Guid } from 'guid-typescript';

@Component({
  selector: 'app-comments-selector',
  templateUrl: './comments-view.component.html',
  styleUrls: ['./comments-view.component.scss']
})
export class CommentsViewComponent implements OnInit {
  @Input()
  comments: IComment[];

  @Input()
  gameId: Guid;

  isDisplayed: Map<number, boolean>;
  isFormDisplayed: Map<number, boolean>;

  display(i: number) {
    this.isDisplayed[i] = !this.isDisplayed[i];
  }
  displayForm(i: number) {
    this.isFormDisplayed[i] = !this.isFormDisplayed[i];
  }
  ngOnInit(): void {
    this.isDisplayed = new Map<number, boolean>();
    this.comments.forEach((c, i) => this.isDisplayed.set(i, false));

    this.isFormDisplayed = new Map<number, boolean>();
    this.comments.forEach((c, i) => this.isFormDisplayed.set(i, false));
  }
}
