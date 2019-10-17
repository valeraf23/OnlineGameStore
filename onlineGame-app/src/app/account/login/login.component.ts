import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { OpenIdConnectService } from 'src/app/core/authentication/open-id-connect.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  constructor(
    private authService: OpenIdConnectService,
    private spinner: NgxSpinnerService
  ) {}

  title = 'Login';

  login() {
    this.spinner.show();
    this.authService.login();
  }

  ngOnInit() {}
}
