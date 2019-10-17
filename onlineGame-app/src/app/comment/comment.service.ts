import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { IComment } from '../models/IComment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  comment: IComment;

  private commentsSource = new Subject<IComment>();

  commentsQuery$ = this.commentsSource.asObservable();

  sendComment(): void {
    this.commentsSource.next();
  }

  setComment(c: IComment) {
    this.comment = c;
  }

  getComment(): IComment {
    return this.comment;
  }
}
