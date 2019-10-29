import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { Error404Component } from './errors/404.component';
import { SharedModule } from './shared/shared.module';

@NgModule({
  imports: [
    RouterModule.forRoot([
      { path: 'home', component: HomeComponent },
      { path: '', redirectTo: 'home', pathMatch: 'full' },
      { path: '**', component: Error404Component }
    ])
  ],
  exports: [
    RouterModule,
    SharedModule]
})
export class AppRoutingModule { }
