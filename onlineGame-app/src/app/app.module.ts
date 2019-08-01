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
import { ConfirmationDialogService } from './games/confirmation-dialog/confirmation-dialog.service';
import { ConfirmationDialogComponent } from './games/confirmation-dialog/confirmation-dialog.component';
import { CanDeactivateGuard } from "./shared/canDeactivateGuard.service";
import { VirtualScrollerModule } from 'ngx-virtual-scroller';
import { CommentsPopup } from "./comment/comments.popup.component";

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
    GameEditComponent,
    ConfirmationDialogComponent,
    CommentsPopup
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    NgxSpinnerModule,
    ReactiveFormsModule,
    VirtualScrollerModule,
    NgMultiSelectDropDownModule.forRoot(),
    RouterModule.forRoot([
      { path: 'games', component: GameListComponent },
      { path: 'new-games', component: GameDetailComponent, canDeactivate: [CanDeactivateGuard] },
      { path: 'games/:id', component: GameEditComponent, canDeactivate: [CanDeactivateGuard]},
      { path: '', redirectTo: 'games', pathMatch: 'full' },
      { path: '**', redirectTo: 'games', pathMatch: 'full' }
    ]),
    NgbModule.forRoot()
  ],
  providers: [ConfirmationDialogService, CanDeactivateGuard],
  entryComponents: [ConfirmationDialogComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
