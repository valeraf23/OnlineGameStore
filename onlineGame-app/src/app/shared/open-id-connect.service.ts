import { Injectable } from '@angular/core';
import { UserManager, User, Log } from 'oidc-client'
import { environment } from '../../environments/environment';
import { ReplaySubject } from 'rxjs/ReplaySubject';

@Injectable({
  providedIn: 'root'
})
export class OpenIdConnectService {

  private userManager: UserManager;
  private user: User;
  userLoaded$ = new ReplaySubject<boolean>(1);

  constructor() {
 
    Log.logger = console;
    this.userManager = new UserManager(environment.openIdConnectSettings);
    this.userManager.clearStaleState();
    this.userManager.getUser().then(user => {
      if (user && !user.expired) {
        this.user = user;
        this.userLoaded$.next(true);
      }
    });
    this.userManager.events.addUserLoaded(user => {
      this.userManager.getUser().then(user => {
        this.user = user;
        this.userLoaded$.next(true);
      });
    });

    this.userManager.events.addUserUnloaded(() => {
      this.user = null;
      this.userLoaded$.next(false);
    });
  }

  handleCallback():void {
    this.userManager.signinRedirectCallback();
  }

  login(): Promise<any> {
    return this.userManager.signinRedirect();
  }

  logout(): Promise<any> {
    return this.userManager.signoutRedirect();
  }

  isLoggedIn(): boolean {
    return this.user && this.user.access_token && !this.user.expired;
  }

  getAccessToken(): string {
    debugger;
    return this.user ? this.user.access_token : '';
  }

  signoutRedirectCallback(): Promise<any> {
    return this.userManager.signoutRedirectCallback();
  }

}

