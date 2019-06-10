import { Component, OnInit } from '@angular/core';
import { IGame } from './gameModel';
import { GameService } from './game.service';

@Component({
  selector: 'app-games',
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.css']
})
export class GameListComponent implements OnInit {
  pageTitle: string = 'Online Games';
  errorMessage: string;
  _listFilter: string;

  get listFilter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    this.filteredGames = this.listFilter ? this.performFilter(this.listFilter) : this.games;
  }

  filteredGames: IGame[];
  games: IGame[] = [];

  constructor(private gameService: GameService) {

  }

  performFilter(filterBy: string): IGame[] {
    filterBy = filterBy.toLocaleLowerCase();
    console.log(this.games[0].name);
    return this.games.filter((g: IGame) =>
      g.name.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  ngOnInit(): void {
    this.gameService.getGames().subscribe(
      games => {
        this.games = games;
        this.filteredGames = this.games;
      },
      error => this.errorMessage = <any>error
    );
  }
}


