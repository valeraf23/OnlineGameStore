import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { Error404Component } from './errors/404.component';
import { SharedModule } from './shared/shared.module';
import { SelectiveStrategy } from './selective-strategy.service';
import { RequireAuthenticatedUserRouteGuardService } from './core/authentication/require-authenticated-user-route-guard.service';

@NgModule({
  imports: [
    RouterModule.forRoot(
      [
        { path: 'home', component: HomeComponent },
        {
          path: 'games',
          canActivate: [RequireAuthenticatedUserRouteGuardService],
          data: { preload: true },
          loadChildren: () =>
            import('./games/game.module').then(m => m.GameModule)
        },
        { path: '', redirectTo: 'home', pathMatch: 'full' },
        { path: '**', component: Error404Component }
      ],
      { preloadingStrategy: SelectiveStrategy }
    )
  ],
  exports: [RouterModule, SharedModule]
})
export class AppRoutingModule {}
