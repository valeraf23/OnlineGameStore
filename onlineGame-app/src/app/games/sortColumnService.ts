import { Injectable } from '@angular/core';
import { IGame} from "./gameModel";
import {ColumnSortedEvent} from "./sortService";

@Injectable({
  providedIn: 'root'
})
export class SortColumnService {

  sort(a: string, b: string): number {
    if (a > b) {
      return 1;
    }
    if (a < b) {
      return -1;
    }
    return 0;
  }

  getGames(games: IGame[], criteria: ColumnSortedEvent): IGame[] {
    return games.sort((a, b) => {
      if (criteria.sortDirection === 'desc') {
        return -1 * this.sort(this.getPropertyByKeyPath(a, criteria.sortColumn), this.getPropertyByKeyPath(b, criteria.sortColumn));
      } else {
        return this.sort(this.getPropertyByKeyPath(a, criteria.sortColumn),
          this.getPropertyByKeyPath(b, criteria.sortColumn));
      }
    });
  }

  getPropertyByKeyPath(targetObj, keyPath:string):string {
    let keys = keyPath.split('.');
    if (keys.length === 0) return undefined;
    keys = keys.reverse();
    let subObject = targetObj;
    while (keys.length) {
      const k = keys.pop();
      if (!subObject.hasOwnProperty(k)) {
        return undefined;
      } else {
        subObject = subObject[k];
      }
    }
    return subObject;
  }

}
