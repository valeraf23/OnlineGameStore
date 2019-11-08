import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { AddAuthorizationHeaderInterceptor } from './core/authentication/add-authorization-header-interceptor';
import { HomeComponent } from './home/home.component';
import { RequireAuthenticatedUserRouteGuardService } from './core/authentication/require-authenticated-user-route-guard.service';
import { HttpErrorInterceptor } from './core/http-error.interceptor';
import { OpenIdConnectService } from './core/authentication/open-id-connect.service';
import { Error404Component } from './errors/404.component';
import { AppRoutingModule } from './app-route.module';
import { AccountModule } from './account/account.module';
import { SharedModule } from './shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    Error404Component,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    SharedModule,
    HttpClientModule,
    AccountModule,
    AppRoutingModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AddAuthorizationHeaderInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true
    },
    OpenIdConnectService,
    RequireAuthenticatedUserRouteGuardService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
