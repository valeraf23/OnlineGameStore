import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
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
import { GameLookComponent } from "./games/games-looks-description/game-look.component";
import { CommentsView} from "./comment/commentView/comments-view.component";
import { CommentView } from "./comment/commenView/comment-view.component";
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ChatComponent} from "./comment/chat.component/chat.component";
import { OpenIdConnectService} from "./shared/open-id-connect.service";
import { AddAuthorizationHeaderInterceptor } from './shared/add-authorization-header-interceptor';
import { HomeComponent } from './home/home.component';
import { UnauthorizedComponent} from "./unauthorized/unauthorized.component";


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    GameListComponent,
    JoinStringArrayPipe,
    GameDetailComponent,
    GenreDetailComponent,
    NgbdPaginationAdvanced,
    SortableColumnComponent,
    SortableTableDirective,
    GameEditComponent,
    ConfirmationDialogComponent,
    CommentsPopup,
    GameLookComponent,
    CommentsView,
    CommentView,
    ChatComponent,
    UnauthorizedComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    NgxSpinnerModule,
    ReactiveFormsModule,
    FontAwesomeModule,
    VirtualScrollerModule,
    NgMultiSelectDropDownModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      { path: 'games', component: GameListComponent },
      { path: 'new-games', component: GameDetailComponent, canDeactivate: [CanDeactivateGuard] },
      { path: 'games/:id', component: GameEditComponent, canDeactivate: [CanDeactivateGuard] },
      { path: 'games/:id/comments/:commentId', component: CommentView },
      { path: 'description/:id', component: GameLookComponent },
      { path: 'unauthorized', component: UnauthorizedComponent },
      { path: '', redirectTo: 'games', pathMatch: 'full' },
      { path: '**', redirectTo: 'games', pathMatch: 'full' },
    ]),
    NgbModule.forRoot()
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
  useClass: AddAuthorizationHeaderInterceptor,
  multi: true
    },
    ConfirmationDialogService, CanDeactivateGuard, OpenIdConnectService],
  entryComponents: [ConfirmationDialogComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
