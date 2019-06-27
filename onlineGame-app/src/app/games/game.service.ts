import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpResponse, HttpHeaders} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap, map } from 'rxjs/operators';

import { IGame, IPlatformType, IGenre, System } from './gameModel';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  postGame(stringify: string) {
    console.log(stringify);
    let options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post('api/games', stringify, options)
      .pipe(catchError(this.handleError));
  }

  putGame(id: string,stringify: string) {
    console.log(stringify);
    let options = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put(`api/games/${id}`, stringify, options)
      .pipe(catchError(this.handleError));
  }
  constructor(private http: HttpClient) { }

  getPageGames(page: number): Observable<HttpResponse<IGame[]>> {
    return this.http.get<IGame[]>(`api/games?pageNumber=${page}`, { observe: 'response' })
      .pipe(tap(data => { console.log(`api/games?pageNumber=${page}`) }), catchError(this.handleError));
  }

  getGame(id: string): Observable<IGame>{
    return this.http.get<IGame>(`api/games/${id}`)
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
