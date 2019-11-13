import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { IGenre } from '../models/IGenre';
import { IPlatformType } from '../models/IPlatformType';
import { IComment } from '../models/IComment';
import { IGame } from '../models/IGame';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  postGame(stringify: string) {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.post('api/games', stringify, options);
  }

  putGame(id: string, stringify: string) {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.put(`api/games/${id}`, stringify, options);
  }
  constructor(private http: HttpClient) {}

  getPageGames(page: number, query: string): Observable<HttpResponse<IGame[]>> {
    return this.http
      .get<IGame[]>(`api/games?pageNumber=${page}&searchQuery=${query}`, {
        observe: 'response'
      })
      .pipe(
        tap(data => {
          console.log(`api/games?pageNumber=${page}&searchQuery=${query}`);
        })
      );
  }

  getGame(id: string): Observable<IGame> {
    return this.http.get<IGame>(`api/games/${id}`);
  }

  delete(id: string): Observable<any> {
    return this.http.delete(`api/games/${id}`);
  }

  getComment(id: string, commentsId: string): Observable<IComment> {
    return this.http.get<IComment>(`api/games/${id}/comments/${commentsId}`);
  }

  getComments(id: string): Observable<IComment[]> {
    return this.http.get<IComment[]>(`api/games/${id}/comments`);
  }

  addCommentToGame(idGame: string, comment: string): Observable<any> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.post(`api/games/${idGame}/comments`, comment, options);
  }

  addAnswerToComment(
    idGame: string,
    commentId: string,
    comment: string
  ): Observable<any> {
    const options = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    };
    return this.http.post(`api/games/${idGame}/comments/${commentId}`, comment, options);
  }

  getPlatformTypes(): Observable<IPlatformType[]> {
    return this.http.get<IPlatformType[]>(`api/platformType`);
  }

  getGenres(): Observable<IGenre[]> {
    return this.http.get<IGenre[]>(`api/genres`);
  }
}
