import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'joinStr'
})
export class JoinStringArrayPipe implements PipeTransform {

  transform(value: string[], key: string, character: string): string {
    return value.map(x => `${x[key]}`).join(character);
  }
}
