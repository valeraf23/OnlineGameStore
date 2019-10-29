import { Component, OnInit } from '@angular/core';
import { OpenIdConnectService } from 'src/app/core/authentication/open-id-connect.service';

@Component({
  selector: 'app-unauthorized',
  templateUrl: 'unauthorized.component.html'
})

export class UnauthorizedComponent implements OnInit {
  constructor(private authService: OpenIdConnectService) { }

  ngOnInit() { }

  logout() {
    this.authService.logout();
  }
}
