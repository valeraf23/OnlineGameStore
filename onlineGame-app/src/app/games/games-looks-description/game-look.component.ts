import { Component, OnInit, AfterViewInit} from '@angular/core';
import { IGame, IGenre } from "../gameModel";
import { GameService } from "../game.service";
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';

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
    this.router.navigate(['/games']);
  }

  getSubGenres(genres: IGenre[]): string {
    return genres.map(x => `${x.subGenres.map(e => e.name)}`).join(';');
  }

  ngOnInit() {
    this.spinner.show();
    this.id = this.route.snapshot.paramMap.get("id");
  }

}

