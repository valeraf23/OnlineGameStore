import { Injectable } from '@angular/core';
import { OpenIdConnectService } from './authentication/open-id-connect.service';
import { Role } from './authentication/role.config';

@Injectable({
  providedIn: 'root'
})
export class PolicesService {

  constructor(private openIdConnectService: OpenIdConnectService) { }

  isAdmin(): boolean {
    return this.guard() &&
      !!(this.openIdConnectService.authContext.claims.find(
        c => c.type === Role.admin.type && c.value === Role.admin.value));
  }

  canEdit(publisherId: string): boolean {
    if (!this.guard()) {
      return false;
    }
    if (this.isAdmin()) {
      return true;
    }

    return this.openIdConnectService.authContext.userProfile.id === publisherId;
  }

  private guard(): boolean {
    return !!this.openIdConnectService.authContext &&
      !!this.openIdConnectService.authContext.claims;
  }
}
