import { Component, AfterViewInit, OnInit } from '@angular/core';
import { IComment } from "../../games/gameModel";
import { CommentService } from 'src/app/games/comment.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute } from '@angular/router';
import { GameService } from "../../games/game.service";
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'comment',
  templateUrl: './comment-view.component.html',
  styleUrls: ['./comment-view.component.scss']
})
export class CommentView implements OnInit, AfterViewInit {

  comment: IComment;
  id: string;
  isDisplayed: boolean;

  constructor(private commentService: CommentService, private spinner: NgxSpinnerService,
    private route: ActivatedRoute, private gameService: GameService) {  }


  ngAfterViewInit(): void {
    if (!this.comment) {
      const gameId = this.route.snapshot.paramMap.get("id");
      const commentId = this.route.snapshot.paramMap.get("commentId");
        this.gameService.getComment(gameId, commentId)
          .pipe(finalize(() => {
            this.spinner.hide();
          }))
        .subscribe(
          pt => {
            this.comment = pt;
            this.spinner.hide();
          }
        );
    } else {
      this.spinner.hide();
    }
  }

  display() {
    this.isDisplayed = !this.isDisplayed;
  }

  ngOnInit() {
    this.spinner.show();
    this.comment = this.commentService.getComment();
  }
}
