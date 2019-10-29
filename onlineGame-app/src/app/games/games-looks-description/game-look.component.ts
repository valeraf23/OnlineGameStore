import { Component, OnInit, AfterViewInit} from '@angular/core';
import { GameService } from '../../core/game.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { IGame } from 'src/app/models/IGame';
import { IGenre } from 'src/app/models/IGenre';


@Component({
  templateUrl: './game-look.component.html',
})

export class GameLookComponent implements OnInit, AfterViewInit {

  pageTitle = 'Game Details';
  id: string;
  game: IGame;
  errorMessage: string;

  constructor(private spinner: NgxSpinnerService,
              private gameService: GameService,
              private route: ActivatedRoute, private router: Router) {
  }

  ngAfterViewInit(): void {
    this.gameService.getGame(this.id)
      .subscribe(
        pt => {
          this.game = pt;
          this.spinner.hide();
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
  }

}

