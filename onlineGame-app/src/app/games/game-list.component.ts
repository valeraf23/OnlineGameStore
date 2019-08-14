import { Component, OnInit } from '@angular/core';
import { IGame } from './gameModel';
import { GameService } from './game.service';
import { IGenre } from './gameModel';
import {SortColumnService} from "./sortColumnService";
import {ColumnSortedEvent} from "./sortService";
import { Page } from "./gameModel";
import { NgxSpinnerService } from 'ngx-spinner';
import { GameSearchService} from "./game.search.service";

@Component({
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.css']
})
export class GameListComponent implements OnInit {
  pageTitle: string = 'Online Games';
  errorMessage: string;
  _listFilter: string;
  private queryString="";

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
  pages: number = 1;
  commentsIndex: number = -1;

  constructor(private spinner: NgxSpinnerService,
    private gameService: GameService,
    private service: SortColumnService,
    private searchQueryService: GameSearchService) {
  }

  visibleComments(value: number) {
    if (this.commentsIndex === value) {
      this.commentsIndex = -1;
    } else {
      this.commentsIndex = value;
    }
  }

  isPopUpVisible(i: number): boolean {
    return this.commentsIndex === i;
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
    const g = this.service.getGames(games, criteria);
    this.games = g;
    this.filteredGames = g;
  }

  onSorted($event: ColumnSortedEvent) {
    this.getGames(this.filteredGames, $event);
  }

  getPageFromService($event) {
    this.getPages($event);
  }

  searchQuery() {
    debugger;
    this.searchQueryService.searchQuery$.subscribe(query => {
      debugger;
      this.queryString = query;
      this.getPages(1);
    });
  }

  getPages(pageNumber: number) {
    this.spinner.show();
    debugger;
    this.gameService.getPageGames(pageNumber, this.queryString).subscribe(
      games => {
        console.log(games.headers.get('x-pagination'));
        this.page = Object.assign(new Page(), JSON.parse(games.headers.get('x-pagination')));
        this.getGames([...games.body], { sortColumn: 'id', sortDirection: 'asc' });
        this.spinner.hide();
      },
      error => this.errorMessage = error
    );
  }

  ngOnInit(): void {
    debugger;
    this.searchQuery();
  }

  ngAfterViewInit(): void {
    debugger;
    this.getPages(1);
  }
}



