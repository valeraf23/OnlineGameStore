import { Component } from '@angular/core';

@Component({
  selector: 'pm-root',
  template: `
    <nav class='navbar navbar-expand-lg navbar-dark bg-dark'>
      <a class='navbar-brand' href="#">{{pageTitle}}</a>
      <ul class='navbar-nav mr-auto'>
        <li class="nav-item"><a class='nav-link' routerLinkActive="active" [routerLinkActiveOptions]="{exact: true}" [routerLink]="['/games']">Games</a></li>
        <li class="nav-item"><a class='nav-link' routerLinkActive="active" [routerLinkActiveOptions]="{exact: true}" [routerLink]="['/new-games']">Create</a></li>
      </ul>
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
}
