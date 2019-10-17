import { Guid } from 'guid-typescript';

export interface IComment {
  id: Guid;
  name: string;
  body: string;
  gameId: Guid;
  parentComment: IComment;
  answers: IComment[];
}
