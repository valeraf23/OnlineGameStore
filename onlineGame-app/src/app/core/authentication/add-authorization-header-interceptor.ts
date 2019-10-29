import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpInterceptor,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { OpenIdConnectService } from './open-id-connect.service';
import 'rxjs/add/operator/do';
import { Router } from '@angular/router';

@Injectable()
export class AddAuthorizationHeaderInterceptor implements HttpInterceptor {
  constructor(
    private openIdConnectService: OpenIdConnectService,
    private router: Router
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (request.url.startsWith('api')) {
      const accessToken = this.openIdConnectService.getAccessToken();
      const headers = request.headers.set(
        'Authorization',
        `Bearer ${accessToken}`
      );
      request = request.clone({ headers });
      return next.handle(request).do(
        () => {},
        error => {
          const respError = error as HttpErrorResponse;
          if (
            respError &&
            (respError.status === 401 || respError.status === 403)
          ) {
            this.router.navigateByUrl('/account/unauthorized');
          }
        }
      );
    } else {
      return next.handle(request);
    }
  }
}
