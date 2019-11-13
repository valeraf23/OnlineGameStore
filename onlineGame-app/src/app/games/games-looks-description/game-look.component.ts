import { Component, OnInit, AfterViewInit} from '@angular/core';
import { GameService } from '../../core/game.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { IGame } from 'src/app/models/IGame';
import { IGenre } from 'src/app/models/IGenre';
import { finalize } from 'rxjs/operators';
import { IComment } from 'src/app/models/IComment';
import { CommentsService } from 'src/app/comment/comments.service';


@Component({
  templateUrl: './game-look.component.html',
})

export class GameLookComponent implements OnInit, AfterViewInit {

  pageTitle = 'Game Details';
  id: string;
  game: IGame;
  comments: IComment[];
  errorMessage: string;

  constructor(private spinner: NgxSpinnerService,
              private comentService: CommentsService,
              private gameService: GameService,
              private route: ActivatedRoute, private router: Router) {
  }

  ngAfterViewInit(): void {
    this.gameService.getGame(this.id)
    .pipe(
      finalize(() => {
        this.spinner.hide();
      })
    )
      .subscribe(
        pt => {
          this.game = pt;
          this.comments = pt.comments;
        },
        error => this.errorMessage = error
      );
  }

  onBack(): void {
    this.router.navigate(['/games'], {queryParamsHandling: 'preserve'});
  }

  getSubGenres(genres: IGenre[]): string {
    return genres.map(x => `${x.subGenres.map(e => e.name)}`).join(';');
  }

  ngOnInit() {
    this.spinner.show();
    this.id = this.route.snapshot.paramMap.get('id');
    this.comentService.searchQuery$.subscribe(c => this.comments = c);
  }
}

