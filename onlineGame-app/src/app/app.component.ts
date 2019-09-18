import { Component } from '@angular/core';
import { GameSearchService } from "./games/game.search.service";

@Component({
  selector: 'pm-root',
  styles:[`input[type="search"]::-webkit-search-cancel-button {-webkit-appearance: searchfield-cancel-button;}`],
  template: `
    <nav class='navbar navbar-expand-lg navbar-dark bg-dark'>
      <a class='navbar-brand' href="#">{{pageTitle}}</a>
      <ul class='navbar-nav mr-auto'>
        <li class="nav-item"><a class='nav-link' routerLinkActive="active" [routerLinkActiveOptions]="{exact: true}" [routerLink]="['/games']">Games</a></li>
        <li class="nav-item"><a class='nav-link' routerLinkActive="active" [routerLinkActiveOptions]="{exact: true}" [routerLink]="['/new-games']">Create</a></li>
      </ul>

      <form class="form-inline" (ngSubmit)="searchGames(searchQuery)">      
        <div class="form-group mx-sm-3 mb-2">
          <input type="search" [(ngModel)]="searchQuery" name="searchQuery" class="form-control" placeholder="Search Games">
          <span
            class="glyphicon glyphicon-remove form-control-feedback" 
            *ngIf="searchQuery?.length" (click)="searchQuery=''">
          </span>
        </div>
        <button type="submit" class="btn btn-primary mb-2">Search</button>
      </form>

    </nav>
    <div class='container'>
      <ngx-spinner [fullScreen]="false" type="timer" size="medium"><p style="color: white" > Loading... </p>
      </ngx-spinner>
      <router-outlet></router-outlet>
    </div>
`
})
export class AppComponent {

  pageTitle: string = 'OGS';
  searchQuery:string;
  constructor(private gameSearchService: GameSearchService) {}

  searchGames(searchQuery) {
    debugger 
    this.gameSearchService.sendMessage(searchQuery);
  };
}

