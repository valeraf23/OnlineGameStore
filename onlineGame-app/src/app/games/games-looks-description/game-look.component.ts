import { Component, OnInit, AfterViewInit} from '@angular/core';
import { IGame,IComment } from "../gameModel";
import { GameService } from "../game.service";
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute } from '@angular/router';

@Component({
  templateUrl: './game-look.component.html',
})

export class GameLookComponent implements OnInit, AfterViewInit {

  id: string;
  game: IGame;
  errorMessage: string;

  constructor(private spinner: NgxSpinnerService,
    private gameService: GameService,
    private route: ActivatedRoute) {
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

  ngOnInit() {
    this.spinner.show();
    this.id = this.route.snapshot.paramMap.get("id");
  }

}

