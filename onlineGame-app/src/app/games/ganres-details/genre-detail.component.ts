import { Component, Input } from '@angular/core';
import { IGenre } from 'src/app/models/IGenre';

@Component({
  selector: 'app-genre-detail-selector',
  templateUrl: './genre-detail.component.html'
})
export class GenreDetailComponent {
  @Input()
  genres: IGenre[] = [];

  private SubGenres: string;

  get subGenres(): string {
    return `Sub genres: ${this.SubGenres}`;
  }

  @Input()
  set subGenres(value: string) {
    this.SubGenres = value;
  }
}
