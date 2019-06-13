import { Component, Input, OnInit } from '@angular/core';
import {IGenre} from "../gameModel";

@Component({
  selector: 'genres',
  templateUrl: './genre-detail.component.html',
  styleUrls: ['./genre-list.component.css']
})
export class GenreDetailComponent implements OnInit {

  @Input()
  genres: IGenre[]=[];

  _subGenres: string;

  get subGenres(): string {
    return `Sub genres: ${this._subGenres}`;
  }

  @Input()
  set subGenres(value: string) {
    this._subGenres = value;
  }
  ngOnInit(): void {
//    $(document).ready(() => {
//      $('[data-toggle="tooltip"]').tooltip();
//    });
  }
}

