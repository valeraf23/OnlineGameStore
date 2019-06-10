import { Pipe, PipeTransform } from '@angular/core';
import { IGenre } from '../games/gameModel';


@Pipe({
  name: 'joinStr'
})
export class JoinStringArrayPipe implements PipeTransform {

  transform(value: IGenre[], character: string): string {
    return value.map(x => `${x.name} - ${x.subGenres.map(e => e.name).join(character)}`).join(character);
  }
}
