import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OpenIdConnectService } from '../core/authentication/open-id-connect.service';

@Component({
  selector: 'app-home',
  templateUrl: 'home.component.html'
})
export class HomeComponent implements OnInit {
  constructor(
    private openIdConnectService: OpenIdConnectService,
    private router: Router
  ) {}

  ngOnInit() {
    this.openIdConnectService.userLoaded$.subscribe(userLoaded => {
      if (userLoaded) {
        this.router.navigate(['./games']);
      }
    });
  }
}
