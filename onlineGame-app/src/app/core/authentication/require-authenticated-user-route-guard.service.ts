import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { OpenIdConnectService } from './open-id-connect.service';

@Injectable()
export class RequireAuthenticatedUserRouteGuardService implements CanActivate {

  constructor(private openIdConnectService: OpenIdConnectService) { }

  canActivate() {
    if (this.openIdConnectService.isLoggedIn()) {
      return true;
    } else {
      this.openIdConnectService.login();
      return false;
    }
  }
}
