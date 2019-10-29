import { IGame } from './IGame';
import { IGenre } from './IGenre';
import { IPlatformType } from './IPlatformType';

export interface GameResolved {
  resolvedData: {
    game: IGame;
    genres: IGenre[];
    platformTypes: IPlatformType[];
  };
  error?: any;
}
