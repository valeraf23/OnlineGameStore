import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { IComment } from '../models/IComment';

@Injectable({
  providedIn: 'root'
})
export class CommentsService {

  private commentsSource = new Subject<IComment[]>();

  searchQuery$ = this.commentsSource.asObservable();

  addComments(message: IComment[]): void {
    this.commentsSource.next(message);
  }

}
