import { IGenre } from './IGenre';
import { IPlatformType } from './IPlatformType';
import { IPublisher } from './IPublisher';
import { IComment } from './IComment';
import { Guid } from 'guid-typescript';

export interface IGame {
  id: Guid;
  name: string;
  description: string;
  publisher: IPublisher;
  comments: IComment[];
  genres: IGenre[];
  platformTypes: IPlatformType[];
}
