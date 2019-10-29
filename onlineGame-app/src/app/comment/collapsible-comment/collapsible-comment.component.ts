import { Component } from '@angular/core';

@Component({
  selector: 'app-collapsible-comment-selector',
  templateUrl: './collapsible-comment.component.html',
  styleUrls: ['./collapsible-comment.component.css']
})
export class CollapsibleCommentComponent {
  visible = false;
  visibleAnswer = false;

  toggleContent() {
    this.visible = !this.visible;
  }
  toggleContentAnswer() {
    this.visibleAnswer = !this.visibleAnswer;
  }
}
