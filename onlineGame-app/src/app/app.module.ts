import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppComponent } from './app.component';
import { GameListComponent } from './games/game-list.component';
import { JoinStringArrayPipe } from './shared/join-strings-array.pipe';
import { GameDetailComponent } from './games/games-details/game-detail.component';
import { GenreDetailComponent } from './games/ganres-details/genre-detail.component';
import { NgbdPaginationAdvanced } from './shared/pagination/pagination-advanced';
import { SortableColumnComponent} from "./games/SortTableComponent/sortable-column.component";
import { SortableTableDirective } from "./games/SortableTableDirective";
import { NgxSpinnerModule } from 'ngx-spinner';
import { ReactiveFormsModule } from '@angular/forms';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { GameEditComponent} from "./games/games-details/edit-detail.component";

@NgModule({
  declarations: [
    AppComponent,
    GameListComponent,
    JoinStringArrayPipe,
    GameDetailComponent,
    GenreDetailComponent,
    NgbdPaginationAdvanced,
    SortableColumnComponent,
    SortableTableDirective,
    GameEditComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    NgxSpinnerModule,
    ReactiveFormsModule,
    NgMultiSelectDropDownModule.forRoot(),
    RouterModule.forRoot([
      { path: 'games', component: GameListComponent },
      { path: 'new-games', component: GameDetailComponent },
      { path: 'games/:id', component: GameEditComponent },
      { path: '', redirectTo: 'games', pathMatch: 'full' },
      { path: '**', redirectTo: 'games', pathMatch: 'full' }
    ]),
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
