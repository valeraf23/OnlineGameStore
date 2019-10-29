import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Toastr, TOASTR_TOKEN } from './toast.services';

const toastr: Toastr = window['toastr'];

@NgModule({
  imports: [NgxSpinnerModule, NgbModule],
  exports: [CommonModule, NgxSpinnerModule, FormsModule, NgbModule],
  providers: [{ provide: TOASTR_TOKEN, useValue: toastr }]
})
export class SharedModule {}
