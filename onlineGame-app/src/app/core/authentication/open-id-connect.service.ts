import { Injectable } from '@angular/core';
import { UserManager, User, Log } from 'oidc-client';
import { ReplaySubject } from 'rxjs/ReplaySubject';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthContext } from '../../shared/authContext';
import { ConfigService } from '../../../environments/config.service';
import { UserRegistration } from '../../shared/user.registration';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OpenIdConnectService {

  private userManager: UserManager;
  private user: User;
  private AuthContext: AuthContext;
  userLoaded$ = new ReplaySubject<boolean>(1);

  constructor(private http: HttpClient, private configService: ConfigService) {

    Log.logger = console;
    this.userManager = new UserManager(configService.openIdConnectSettings);
    this.userManager.clearStaleState();
    this.userManager.getUser().then(user => {
      if (user && !user.expired) {
        this.user = user;
        this.userLoaded$.next(true);
        this.loadSecurityContext().subscribe(context => {
          this.AuthContext = context;
          this.createNewUserIfNotExist();
        },
          error => console.error(error));
      }
    });

    this.userManager.events.addUserLoaded(user => {
      this.user = user;
      this.userLoaded$.next(true);
      this.loadSecurityContext().subscribe(context => { this.AuthContext = context; }, error => console.error(error));
    });

    this.userManager.events.addUserUnloaded(() => {
      this.user = null;
      this.userLoaded$.next(false);
    });
  }

  handleCallback(): void {
    this.userManager.signinRedirectCallback();
  }

  login(): Promise<any> {
    return this.userManager.signinRedirect();
  }

  private createNewUserIfNotExist(): void {
    this.http.get<boolean>(`api/publishers/available/${this.AuthContext.userProfile.id}`).subscribe(
      (result: boolean) => {
        if (result === false) {
          this.createNewUserInApplication();
        }
      });
  }

  private createNewUserInApplication(): void {
    const stringify = { name: this.AuthContext.userProfile.name, id: this.AuthContext.userProfile.id };
    const model = JSON.stringify(stringify);
    const options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    this.http.post(`api/publishers/create`, model, options)
      .subscribe(res => console.log('HTTP response', res), err => console.log('HTTP response', err));
  }

  logout(): Promise<any> {
    return this.userManager.signoutRedirect();
  }

  isLoggedIn(): boolean {
    return this.user && this.user.access_token && !this.user.expired;
  }

  getAccessToken(): string {
    return this.user ? this.user.access_token : '';
  }

  signoutRedirectCallback(): Promise<any> {
    return this.userManager.signoutRedirectCallback();
  }

  loadSecurityContext(): Observable<AuthContext> {
    return this.http.get<AuthContext>(`api/publishers/AuthContext`);
  }

  get authContext(): AuthContext {
    return this.AuthContext;
  }

  register(userRegistration: UserRegistration) {
    return this.http.post(`${this.configService.authApiURI}/account/register`, userRegistration);
  }
}

