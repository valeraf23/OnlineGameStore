import { IGame } from "../games/gameModel";
import { Injectable } from '@angular/core';
import { Resolve } from '@angular/router';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router/router";
import { Observable } from "rxjs";
import { GameService} from "../games/game.service";


@Injectable()
export class GameListResolver implements Resolve<IGame[]> {

  constructor(private service: GameService) {}

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Object[]> |
                                                                      Promise<Object[]> |
                                                                      Object[] {
    return this.service.getPageGames()
  }

  
}
