import {
  OnInit,
  EventEmitter,
  Output,
  OnDestroy,
  Directive
} from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { SortService } from './sortService';

@Directive({
  selector: '[appSortableTable]'
})
export class SortableTableDirective implements OnInit, OnDestroy {
  constructor(private sortService: SortService) {}

  @Output()
  sorted = new EventEmitter();

  private columnSortedSubscription: Subscription;

  ngOnInit() {
    this.columnSortedSubscription = this.sortService.columnSorted$.subscribe(
      event => {
        this.sorted.emit(event);
      }
    );
  }

  ngOnDestroy() {
    this.columnSortedSubscription.unsubscribe();
  }
}
