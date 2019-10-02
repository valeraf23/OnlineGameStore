import { Component, OnInit } from '@angular/core';
import { OpenIdConnectService } from "../shared/open-id-connect.service";
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: 'home.component.html'
})
export class HomeComponent implements OnInit {

  constructor(private openIdConnectService: OpenIdConnectService, private router: Router) {}

  ngOnInit() {
    this.openIdConnectService.userLoaded$.subscribe((userLoaded) => {
      if (userLoaded) {
        this.router.navigate(['./games']);
      }
    });
  }
}
