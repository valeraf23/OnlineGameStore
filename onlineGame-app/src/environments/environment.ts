import { WebStorageStateStore } from 'oidc-client';

let clientRoot = 'https://localhost:4200'

export const environment = {
  production: false,

  openIdConnectSettings: {
    authority: 'https://localhost:44375/',
    client_id: 'online_game_store_client',
    redirect_uri: `${clientRoot}/assets/signin-oidc.html`,
    scope: 'openid profile onlinegamestoreapi',
    response_type: 'id_token token',
    post_logout_redirect_uri: `${clientRoot}/?postLogout=true`,
    userStore: new WebStorageStateStore({ store: window.localStorage }),
    automaticSilentRenew: true,
    silent_redirect_uri: `${clientRoot}/assets/silent-redirect.html`
  }
};


