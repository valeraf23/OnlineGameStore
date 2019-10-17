import { Guid } from 'guid-typescript';

export interface IGenre {
  id: Guid;
  name: string;
  parentGenre: IGenre;
  subGenres: IGenre[];
}
