import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppComponent } from './app.component';
import { GameListComponent } from './games/game-list/game-list.component';
import { JoinStringArrayPipe } from './core/join-strings-array.pipe';
import { GameDetailComponent } from './games/games-details/game-detail.component';
import { GenreDetailComponent } from './games/ganres-details/genre-detail.component';
import { NgbdPaginationAdvancedComponent } from './games/pagination/pagination-advanced';
import { SortableColumnComponent } from './games/sortTable/sortable-column.component';
import { NgxSpinnerModule } from 'ngx-spinner';
import { ReactiveFormsModule } from '@angular/forms';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';
import { GameEditComponent } from './games/games-details/edit-detail.component';
import { ConfirmationDialogService } from './games/confirmation-dialog/confirmation-dialog.service';
import { ConfirmationDialogComponent } from './games/confirmation-dialog/confirmation-dialog.component';
import { CanDeactivateGuard } from './core/can-deactivate-guard.service';
import { VirtualScrollerModule } from 'ngx-virtual-scroller';
import { CommentsPopupComponent } from './comment/comments.popup.component';
import { GameLookComponent } from './games/games-looks-description/game-look.component';
import { CommentsViewComponent } from './comment/commentView/comments-view.component';
import { CommentViewComponent } from './comment/commenView/comment-view.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ChatComponent } from './comment/chat.component/chat.component';
import { AddAuthorizationHeaderInterceptor } from './core/authentication/add-authorization-header-interceptor';
import { HomeComponent } from './home/home.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { RequireAuthenticatedUserRouteGuardService } from './core/authentication/require-authenticated-user-route-guard.service';
import { AccountModule } from './account/account.module';
import { HttpErrorInterceptor } from './core/http-error.interceptor';
import { SortableTableDirective } from './games/sortTable/SortableTableDirective';
import { OpenIdConnectService } from './core/authentication/open-id-connect.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    GameListComponent,
    JoinStringArrayPipe,
    GameDetailComponent,
    GenreDetailComponent,
    NgbdPaginationAdvancedComponent,
    SortableColumnComponent,
    SortableTableDirective,
    GameEditComponent,
    ConfirmationDialogComponent,
    CommentsPopupComponent,
    GameLookComponent,
    CommentsViewComponent,
    CommentViewComponent,
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
    AccountModule,
    NgMultiSelectDropDownModule.forRoot(),
    RouterModule.forRoot([
      { path: '', component: HomeComponent },
      {
        path: 'games',
        component: GameListComponent,
        canActivate: [RequireAuthenticatedUserRouteGuardService]
      },
      {
        path: 'new-games',
        component: GameDetailComponent,
        canDeactivate: [CanDeactivateGuard],
        canActivate: [RequireAuthenticatedUserRouteGuardService]
      },
      {
        path: 'games/:id',
        component: GameEditComponent,
        canDeactivate: [CanDeactivateGuard],
        canActivate: [RequireAuthenticatedUserRouteGuardService]
      },
      {
        path: 'games/:id/comments/:commentId',
        component: CommentViewComponent,
        canActivate: [RequireAuthenticatedUserRouteGuardService]
      },
      {
        path: 'description/:id',
        component: GameLookComponent,
        canActivate: [RequireAuthenticatedUserRouteGuardService]
      },
      { path: 'unauthorized', component: UnauthorizedComponent },
      { path: '', redirectTo: 'games', pathMatch: 'full' },
      { path: '**', redirectTo: 'games', pathMatch: 'full' }
    ]),
    NgbModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AddAuthorizationHeaderInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true
    },
    ConfirmationDialogService,
    CanDeactivateGuard,
    OpenIdConnectService,
    RequireAuthenticatedUserRouteGuardService
  ],
  entryComponents: [ConfirmationDialogComponent],
  bootstrap: [AppComponent]
})
export class AppModule {}
