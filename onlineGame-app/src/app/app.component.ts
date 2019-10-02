import { Component, OnInit } from '@angular/core';
import { GameSearchService } from "./games/game.search.service";
import { OpenIdConnectService } from "./shared/open-id-connect.service";
import { Router } from '@angular/router';

@Component({
  selector: 'pm-root',
  styles: [`input[type="search"]::-webkit-search-cancel-button {-webkit-appearance: searchfield-cancel-button;}
.logged {
margin-left: 5px            
}
`],
  template: `
    <nav class='navbar navbar-expand-lg navbar-dark bg-dark'>
      <a class='navbar-brand' href="#">{{pageTitle}}</a>
      <ul *ngIf="isLoggedIn()" class='navbar-nav mr-auto'>
        <li class="nav-item"><a class='nav-link' routerLinkActive="active" [routerLinkActiveOptions]="{exact: true}" [routerLink]="['/games']">Games</a></li>
        <li class="nav-item"><a class='nav-link' routerLinkActive="active" [routerLinkActiveOptions]="{exact: true}" [routerLink]="['/new-games']">Create</a></li>
      </ul>

      <form *ngIf="isLoggedIn()" class="form-inline" (ngSubmit)="searchGames(searchQuery)">      
        <div class="form-group mx-sm-3 mb-2">
          <input type="search" [(ngModel)]="searchQuery" name="searchQuery" class="form-control" placeholder="Search Games">
          <span
            class="glyphicon glyphicon-remove form-control-feedback" 
            *ngIf="searchQuery?.length" (click)="searchQuery=''">
          </span>
        </div>
        <button type="submit" class="btn btn-primary mb-2">Search</button>
      </form>
      <span>
        <button class="logged btn btn-info mb-2" *ngIf="!isLoggedIn()" mat-button (click)="login()">Login</button>
        <button class="logged btn btn-info mb-2" *ngIf="isLoggedIn()" mat-button (click)="logout()">Logout</button>
      </span>
    </nav>
    <div class='container'>
      <ngx-spinner [fullScreen]="false" type="timer" size="medium"><p style="color: white" > Loading... </p>
      </ngx-spinner>
      <router-outlet></router-outlet>
    </div>
`
})
export class AppComponent implements OnInit {

  pageTitle = 'OGS';
  searchQuery: string;

  constructor(private route: Router,
    private gameSearchService: GameSearchService,
    private openIdConnectService: OpenIdConnectService) {
  }

  searchGames(searchQuery) {
    this.gameSearchService.sendMessage(searchQuery);
  };

  login() {
    this.openIdConnectService.login();
  }

  logout() {
    this.openIdConnectService.logout();
  }

  isLoggedIn() {
    return this.openIdConnectService.isLoggedIn();
  }

  ngOnInit() {
    if (window.location.href.indexOf('?postLogout=true') > 0) {
      this.openIdConnectService.signoutRedirectCallback().then(() => {
        const url = this.route.url.substring(
          0,
          this.route.url.indexOf('?')
        );
        debugger;
        this.route.navigateByUrl(url);
      });
    }
  }

}

