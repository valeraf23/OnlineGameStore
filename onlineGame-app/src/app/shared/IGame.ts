import { IGenre } from '../models/IGenre';
import { IPlatformType } from '../models/IPlatformType';
import { IPublisher } from '../models/IPublisher';
import { IComment } from '../models/IComment';
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
