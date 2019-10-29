import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs/operators';
import { UserRegistration } from '../user.registration';
import { OpenIdConnectService } from 'src/app/core/authentication/open-id-connect.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  success: boolean;
  error: string;
  userRegistration: UserRegistration = { name: '', email: '', password: '' };
  submitted = false;
  spinnerName = 'register';

  constructor(
    private authService: OpenIdConnectService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit() {}

  onSubmit() {
    this.spinner.show(this.spinnerName);

    this.authService
      .register(this.userRegistration)
      .pipe(
        finalize(() => {
          this.spinner.hide(this.spinnerName);
        })
      )
      .subscribe(
        (result: any) => {
          if (result) {
            this.success = true;
          }
        },
        (error: string) => {
          this.error = error;
        }
      );
  }
}
