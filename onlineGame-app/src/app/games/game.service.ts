import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpResponse, HttpHeaders} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { IGame, IPlatformType, IGenre, IComment } from './gameModel';


@Injectable({
  providedIn: 'root'
})
export class GameService {
  postGame(stringify: string) {
    const options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post('api/games', stringify, options)
      .pipe(catchError(this.handleError));
  }

  putGame(id: string,stringify: string) {
    const options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put(`api/games/${id}`, stringify, options)
      .pipe(catchError(this.handleError));
  }
  constructor(private http: HttpClient) { }

  getPageGames(page: number, query:string): Observable<HttpResponse<IGame[]>> {
    return this.http.get<IGame[]>(`api/games?pageNumber=${page}&searchQuery=${query}`, { observe: 'response' })
      .pipe(tap(data => { console.log(`api/games?pageNumber=${page}&searchQuery=${query}`)}), catchError(this.handleError));
  }

  getGame(id: string): Observable<IGame>{
    return this.http.get<IGame>(`api/games/${id}`)
      .pipe(catchError(this.handleError));
  }

  getComment(id: string, commentsId: string): Observable<IComment> {
    return this.http.get<IComment>(`api/games/${id}/comments/${commentsId}`)
      .pipe(catchError(this.handleError));
  }

  addCommentToGame(idGame: string, comment: string): Observable<any> {
    const options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post(`api/games/${idGame}/comments`, comment, options)
      .pipe(catchError(this.handleError));
  }

  addAnswerToComment(idGame: string, commentId: string, comment: string): Observable<any> {
    const options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
   return this.http.post(`api/games/${idGame}/comments/${commentId}`, comment, options)
      .pipe(catchError(this.handleError));
  }

  getPlatformTypes(): Observable<IPlatformType[]> {
    return this.http.get<IPlatformType[]>(`api/platformType`)
      .pipe(catchError(this.handleError));
  }

  getGenres(): Observable<IGenre[]> {
    return this.http.get<IGenre[]>(`api/genres`)
      .pipe(catchError(this.handleError));
  }

  private handleError(err: HttpErrorResponse) {
    // in a real world app, we may send the server to some remote logging infrastructure
    // instead of just logging it to the console
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      // A client-side or network error occurred. Handle it accordingly.
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }
}
