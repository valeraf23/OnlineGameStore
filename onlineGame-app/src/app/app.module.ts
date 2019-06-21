import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppComponent } from './app.component';
import { GameListComponent } from './games/game-list.component';
import { JoinStringArrayPipe } from './shared/join-strings-array.pipe';
import { GameDetailComponent } from './games/gamesDetails/game-detail.component';
import { GenreDetailComponent } from './games/ganresDetails/genre-detail.component';
import { NgbdPaginationAdvanced } from './shared/pagination/pagination-advanced';
import { SortableColumnComponent} from "./games/SortTableComponent/sortable-column.component";
import {SortableTableDirective} from "./games/SortableTableDirective";

@NgModule({
  declarations: [
    AppComponent,
    GameListComponent,
    JoinStringArrayPipe,
    GameDetailComponent,
    GenreDetailComponent,
    NgbdPaginationAdvanced,
    SortableColumnComponent,
    SortableTableDirective
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: 'games', component: GameListComponent },
      { path: 'games/:id', component: GameDetailComponent },
      { path: '', redirectTo: 'games', pathMatch: 'full' },
      { path: '**', redirectTo: 'games', pathMatch: 'full' }
    ]),
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
