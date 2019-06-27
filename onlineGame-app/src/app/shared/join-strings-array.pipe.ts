import { Pipe, PipeTransform } from '@angular/core';
import { IGenre } from '../games/gameModel';


@Pipe({
  name: 'joinStr'
})
export class JoinStringArrayPipe implements PipeTransform {

  transform(value: any, key:string, character: string): string {
    return value.map(x => `${x[key]}`).join(character);
  }
}
