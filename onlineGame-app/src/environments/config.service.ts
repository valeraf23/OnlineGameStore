import { Injectable } from '@angular/core';
import { WebStorageStateStore, UserManagerSettings } from 'oidc-client';


@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  production: false;

  get authApiURI(): string {
    return 'https://localhost:44375';
  }

  get resourceApiURI() {
    return 'https://localhost:44320';
  }

  get clientRoot() {
    return 'https://localhost:4200';
  }

  get openIdConnectSettings(): UserManagerSettings {
    return {
      authority: this.authApiURI,
      client_id: 'online_game_store_client',
      redirect_uri: `${this.clientRoot}/assets/signin-oidc.html`,
      scope: 'openid profile onlinegamestoreapi',
      response_type: 'id_token token',
      post_logout_redirect_uri: `${this.clientRoot}/?postLogout=true`,
      userStore: new WebStorageStateStore({ store: window.localStorage }),
      automaticSilentRenew: true,
      silent_redirect_uri: `${this.clientRoot}/assets/silent-redirect.html`
    }
  };
}
