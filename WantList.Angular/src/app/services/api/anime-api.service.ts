import { Injectable } from '@angular/core';
import { HttpClientService } from './http-client.service';
import { RestApiService } from './rest-api.service';
import { IAnimeApi } from './ianime-api';
import { Anime } from '../../entities/anime';
import { Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AnimeApiService extends IAnimeApi {

  private readonly ANIME_URL = 'anime';

  constructor( private http: HttpClientService, private restApiService: RestApiService ) {
    super();
  }

  getAnimes(): Observable<Anime[]> {
    return this.http.get<Anime[]>({
      url: this.getUrl()
    })
      .pipe(
        catchError(this.restApiService.handleError('getAnimes()', []))
      );
  }

  getAnime( id: number ): Observable<Anime> {
    return this.http.get<Anime>({
      url: this.getUrl(id)
    })
      .pipe(
        catchError(this.restApiService.handleError(`getAnime(${id})`, null))
      );
  }

  getAnimeImage( anime: Anime ): string {
    return this.getImageUrl(anime.anidbId);
  }

  add( anime: Anime ): Observable<Anime> {
    return this.http.post<Anime>({
      url: this.getUrl(),
      body: anime
    })
      .pipe(
        catchError(this.restApiService.handleError(`AnimeApi.add(${anime})`, null))
      );
  }

  update( anime: Anime ): Observable<Anime> {
    return this.http.put<Anime>({
      url: this.getUrl(),
      body: anime
    })
      .pipe(
        catchError(this.restApiService.handleError(`AnimeApi.update(${anime.id})`, null))
      );
  }

  delete( anime: Anime ): Observable<Anime> {
    return this.http.delete<Anime>({
      url: this.getUrl(anime.id)
    })
      .pipe(
        catchError(this.restApiService.handleError(`AnimeApi.delete(${anime.id})`, null))
      );
  }

  private getBaseUrl(): string {
    return `${this.restApiService.url}${this.ANIME_URL}`;
  }

  private getUrl( id: number = 0 ): string {
    return `${this.getBaseUrl()}` + (id !== 0 ? `/${id}` : '');
  }

  private getImageUrl( anidbId: number ): string {
    return `${this.getBaseUrl()}/file/` + anidbId;
  }
}
