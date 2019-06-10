import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { GameListComponent } from './games/game-list.component';
import { JoinStringArrayPipe } from './shared/join-strings-array.pipe';


@NgModule({
  declarations: [
    AppComponent,
    GameListComponent,
  JoinStringArrayPipe
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
