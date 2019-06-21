import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Injectable({
  providedIn: 'root'
})
export class SortService {

  private columnSortedSource = new Subject<ColumnSortedEvent>();

  columnSorted$ = this.columnSortedSource.asObservable();

  columnSorted(event: ColumnSortedEvent) {
    console.log("SortSe");
    console.log(event);
    this.columnSortedSource.next(event);
  }

}

export interface ColumnSortedEvent {
  sortColumn: string;
  sortDirection: string;
}
