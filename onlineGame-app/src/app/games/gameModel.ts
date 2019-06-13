import Guid = System.Guid;

export interface IGame {
  id: Guid;
  name: string;
  description: string;
  publisher: IPublisher;
  comments: IComment[];  
  genres: IGenre[];
  platformTypes: IPlatformType[] ;

}
export interface IComment {
  id: Guid;
  name: string;
  body: string;
  gameId: Guid;
  parentComment: IComment;
  answers: IComment[];
}
export interface IGenre {
  id: Guid;
  name: string;
  parentGenre: IGenre;
  subGenres: IGenre[];
}
export interface IPublisher {
  id: Guid;
  name: string;
}
export interface IPlatformType {
  id: Guid;
  type: string;
}
export module System {
  export class Guid {
    constructor(public guid: string) {
      this._guid = guid;
    }

    private _guid: string;

    public ToString(): string {
      return this.guid;
    }

    // Static member
    static MakeNew(): Guid {
      var result: string;
      var i: string;
      var j: number;

      result = "";
      for (j = 0; j < 32; j++) {
        if (j == 8 || j == 12 || j == 16 || j == 20)
          result = result + '-';
        i = Math.floor(Math.random() * 16).toString(16).toUpperCase();
        result = result + i;
      }
      return new Guid(result);
    }
  }
}
