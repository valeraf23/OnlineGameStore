import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GameListComponent } from './game-list/game-list.component';
import { CanDeactivateGuard } from '../core/can-deactivate-guard.service';
import { GameInfoResolver } from './edit/game-info-resolver.service';
import { GameLookComponent } from './games-looks-description/game-look.component';
import { JoinStringArrayPipe } from '../core/join-strings-array.pipe';
import { GenreDetailComponent } from './ganres-details/genre-detail.component';
import { NgbdPaginationAdvancedComponent } from './pagination/pagination-advanced';
import { SortableColumnComponent } from './sortTable/sortable-column.component';
import { SortableTableDirective } from './sortTable/SortableTableDirective';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { ConfirmationDialogService } from './confirmation-dialog/confirmation-dialog.service';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ReactiveFormsModule } from '@angular/forms';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { SharedModule } from '../shared/shared.module';
import { CommentsViewComponent } from '../comment/commentView/comments-view.component';
import { CommentsPopupComponent } from '../comment/comments.popup.component';
import { ChatComponent } from '../comment/chat.component/chat.component';
import { VirtualScrollerModule } from 'ngx-virtual-scroller';
import { GameEditComponent } from './edit/game-edit.component';
import { ErrorHighlightDirective } from './edit/ng-multiselect-dropdown-directives';
import { CollapsibleComponent } from '../comment/collabsible/collapsible.component';
import { CommentsService } from '../comment/comments.service';


const routes: Routes = [
  {
    path: '',
    component: GameListComponent
  },
  {
    path: ':id/edit',
    component: GameEditComponent,
    canDeactivate: [CanDeactivateGuard],
    resolve: { info: GameInfoResolver }
  },
  {
    path: ':id',
    component: GameLookComponent
  }
];

@NgModule({
  imports: [
    SharedModule,
    VirtualScrollerModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    FontAwesomeModule,
    NgMultiSelectDropDownModule.forRoot()
  ],
  exports: [],
  declarations: [
    CollapsibleComponent,
    CommentsPopupComponent,
    CommentsViewComponent,
    ChatComponent,
    JoinStringArrayPipe,
    GenreDetailComponent,
    NgbdPaginationAdvancedComponent,
    SortableColumnComponent,
    SortableTableDirective,
    ErrorHighlightDirective,
    ConfirmationDialogComponent,
    GameEditComponent,
    GameLookComponent,
    GameEditComponent,
    GameListComponent
  ],
  providers: [ConfirmationDialogService, CanDeactivateGuard, CommentsService],
  entryComponents: [ConfirmationDialogComponent]
})
export class GameModule {}
