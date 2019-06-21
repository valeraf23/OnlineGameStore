import { Component, OnInit, Input } from '@angular/core';
import { IGame } from './gameModel';
import { GameService } from './game.service';
import { IGenre } from './gameModel';
import {SortColumnService} from "./sortColumnService";
import {ColumnSortedEvent} from "./sortService";
import { Page} from "./gameModel";

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
  page: Page = new Page();
  pages:number=1;
  constructor(private gameService: GameService, private service: SortColumnService) {

  }

  performFilter(filterBy: string): IGame[] {
    filterBy = filterBy.toLocaleLowerCase();
    console.log(this.games[0].name);
    return this.games.filter((g: IGame) =>
      g.name.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  getSubGenres(genres: IGenre[]): string {
    return genres.map(x => `${x.subGenres.map(e => e.name)}`).join(';');
  }

  getGames(games: IGame[], criteria: ColumnSortedEvent) {
    console.log(criteria);
    const g = this.service.getGames(games, criteria);
    this.games = g;
    this.filteredGames = g;
  }

  onSorted($event) {
    this.getGames(this.filteredGames,$event);
  }

  getPageFromService($event) {  
    this.getPages($event);
  }

  getPages(pageNumber: number) {
    this.gameService.getPageGames(pageNumber).subscribe(
      games => {
        this.page = Object.assign(new Page(), JSON.parse(games.headers.get('x-pagination')));
        this.getGames([...games.body], { sortColumn: 'id', sortDirection: 'asc' });
      },
      error => this.errorMessage = error
    );
  }

  ngOnInit(): void {
    this.getPages(1);
  }
}



