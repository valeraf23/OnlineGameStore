import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Injectable({
  providedIn: 'root'
})
export class GameSearchService {

  private searchQuerySource = new Subject<string>();

  searchQuery$ = this.searchQuerySource.asObservable();

  sendMessage(message: string): void {
    this.searchQuerySource.next(message);
  }

}
