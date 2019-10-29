import { NgModule } from '@angular/core';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { RouterModule } from '@angular/router';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { SharedModule } from '../shared/shared.module';


@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    UnauthorizedComponent
  ],
  imports: [
    SharedModule,
    RouterModule.forChild([
      { path: 'account/login', component: LoginComponent },
      { path: 'account/register', component: RegisterComponent },
      { path: 'account/unauthorized', component: UnauthorizedComponent }])
  ]

})
export class AccountModule { }
