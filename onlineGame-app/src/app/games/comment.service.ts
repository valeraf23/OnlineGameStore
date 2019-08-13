import { IComment } from "../games/gameModel";
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  comment: IComment;

  setComment(c: IComment) {
    this.comment = c;
  }
  getComment(): IComment { return this.comment; } 
  
}
