import { Component, OnInit, AfterViewInit, OnChanges } from '@angular/core';
import { GameService } from '../../core/game.service';
import { SortColumnService } from '../sortTable/sortColumnService';
import { NgxSpinnerService } from 'ngx-spinner';
import { GameSearchService } from '../../core/game-search.service';
import { PolicesService } from '../../core/polices.service';
import { finalize } from 'rxjs/operators';
import { IGame } from 'src/app/models/IGame';
import { Page } from 'src/app/models/Page';
import { IGenre } from 'src/app/models/IGenre';
import { ColumnSortedEvent } from '../sortTable/sortService';
import { Guid } from 'guid-typescript';
import { ActivatedRoute } from '@angular/router';

@Component({
  templateUrl: './game-list.component.html',
  styleUrls: ['./game-list.component.css']
})
export class GameListComponent implements OnInit, AfterViewInit {
  pageTitle = 'Online Games';
  errorMessage: string;
  ListFilter: string;
  private queryString = '';

  get listFilter(): string {
    return this.ListFilter;
  }

  set listFilter(value: string) {
    this.ListFilter = value;
    this.filteredGames = this.listFilter
      ? this.performFilter(this.listFilter)
      : this.games;
  }

  filteredGames: IGame[];
  games: IGame[] = [];
  page: Page = new Page();
  pages = 1;
  commentsIndex = -1;

  constructor(
    private route: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private gameService: GameService,
    private service: SortColumnService,
    private searchQueryService: GameSearchService,
    private policesService: PolicesService
  ) { }

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
    return this.games.filter(
      (g: IGame) => g.name.toLocaleLowerCase().indexOf(filterBy) !== -1
    );
  }

  getSubGenres(genres: IGenre[]): string {
    return genres.map(x => `${x.subGenres.map(e => e.name)}`).join(';');
  }

  getGames(games: IGame[], criteria: ColumnSortedEvent) {
    const g = this.service.getGames(games, criteria);
    this.games = g;
    this.filteredGames = this.performFilter(this.listFilter);
  }

  deleteGame(id: string) {
    this.gameService.delete(id).subscribe(_ => this.getPages(1));
  }

  onSorted($event: ColumnSortedEvent) {
    this.getGames(this.filteredGames, $event);
  }

  getPageFromService($event: number) {
    this.getPages($event);
  }

  canEditProject(publisherId: Guid): boolean {
    return this.policesService.canEdit(publisherId.toString());
  }

  searchQuery() {
    this.searchQueryService.searchQuery$.subscribe(query => {
      this.queryString = query;
      this.getPages(1);
    });
  }

  getPages(pageNumber: number) {
    this.spinner.show();
    this.gameService
      .getPageGames(pageNumber, this.queryString)
      .pipe(
        finalize(() => {
          this.spinner.hide();
        })
      )
      .subscribe(
        games => {
          this.page = Object.assign(new Page(), JSON.parse(games.headers.get('x-pagination')));
          this.getGames([...games.body], {
            sortColumn: 'id',
            sortDirection: 'asc'
          });
        },
        error => {
          this.errorMessage = error;
        }
      );
  }

  ngOnInit(): void {
    this.route.queryParams.subscribe(queryParams => {
      this.listFilter = queryParams.filterBy || '';
    });
    this.searchQuery();
  }

  ngAfterViewInit(): void {
    this.getPages(1);
  }
}
