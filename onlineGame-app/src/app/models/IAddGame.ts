import { Guid } from 'guid-typescript';
export interface IAddGame {
  id: Guid;
  name: string;
  description: string;
  genresId: Guid[];
  platformTypesId: Guid[];
}
