import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { GameService } from 'src/app/core/game.service';
import { forkJoin } from 'rxjs';
import { Guid } from 'guid-typescript';
import { map, catchError } from 'rxjs/operators';
import { GameResolved } from 'src/app/models/game-resolved';
import { IPlatformType } from 'src/app/models/IPlatformType';
import { IGenre } from 'src/app/models/IGenre';
import { IGame } from 'src/app/models/IGame';

@Injectable({ providedIn: 'root' })
export class GameInfoResolver implements Resolve<GameResolved> {
  constructor(private gameService: GameService) {

  }
  resolve(snapshot: ActivatedRouteSnapshot): Observable<GameResolved> {

    const identer = snapshot.paramMap.get('id');
    const id = identer.replace(/[\u200B-\u200D\uFEFF]/g, '');
    if (!Guid.isGuid(id)) {
      const message = `Game id was not a guid: ${id}`;
      console.error(message);
      return of(this.errorResolved(message));
    }
    const guidId = Guid.parse(id);
    let observables = [];
    if (guidId.isEmpty()) {
      observables = [this.gameService.getPlatformTypes(), this.gameService.getGenres()];
    } else {
      observables = [this.gameService.getPlatformTypes(), this.gameService.getGenres(), this.gameService.getGame(id)];
    }

    return forkJoin(observables).pipe(
      map(results => ((this.toGameResolved as any)(...results))),
      catchError(error => {
        const message = `Retrieval error: ${error}`;
        console.error(message);
        return of(this.errorResolved(message));
      })
    );
  }
  toGameResolved(platformTypes: IPlatformType[], genres: IGenre[], game: IGame): GameResolved {
    return {
      resolvedData: {
        game,
        genres,
        platformTypes
      }
    };
  }
  errorResolved(message: string): GameResolved {
    return {
      resolvedData: null,
      error: message
    };
  }
}


