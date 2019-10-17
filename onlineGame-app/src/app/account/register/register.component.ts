import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs/operators';
import { OpenIdConnectService} from "../../shared/open-id-connect.service";
import { UserRegistration} from "../../user.registration";


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  success: boolean;
  error: string;
  userRegistration: UserRegistration = { name: '', email: '', password: '' };
  submitted: boolean = false;
  spinnerName = 'register';

    constructor(private authService: OpenIdConnectService, private spinner: NgxSpinnerService) {

  }

  ngOnInit() {
  }

  onSubmit() {

      this.spinner.show(this.spinnerName);

    this.authService.register(this.userRegistration)
      .pipe(finalize(() => {
          this.spinner.hide(this.spinnerName);
      }))
      .subscribe(
        result => {
          if (result) {
            this.success = true;
          }
        },
        error => {
          this.error = error;
        });
  }
}
