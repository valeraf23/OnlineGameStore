import { Component, OnInit } from '@angular/core';
import { GameSearchService } from './core/game-search.service';
import { Router } from '@angular/router';
import { AuthContext } from './account/authContext';
import { OpenIdConnectService } from './core/authentication/open-id-connect.service';

@Component({
  selector: 'app-root',
  styleUrls: ['./app.component.css'],
  templateUrl: './app.component.html'
})

export class AppComponent implements OnInit {
  pageTitle = 'OGS';
  searchQuery: string;

  constructor(
    private route: Router,
    private gameSearchService: GameSearchService,
    private openIdConnectService: OpenIdConnectService
  ) {}

  get authContext(): AuthContext {
    return this.openIdConnectService.authContext;
  }
  searchGames(searchQuery: string) {
    this.gameSearchService.sendMessage(searchQuery);
  }

  logout() {
    this.openIdConnectService.logout();
  }

  isLoggedIn() {
    return this.openIdConnectService.isLoggedIn();
  }

  async ngOnInit() {
    if (window.location.href.indexOf('?postLogout=true') > 0) {
     await this.openIdConnectService.signoutRedirectCallback();
     const url = this.route.url.substring(0, this.route.url.indexOf('?'));
     this.route.navigateByUrl(url);
      }
    }
  }

