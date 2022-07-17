import { Injectable } from '@angular/core';
import { HttpClientService } from './http-client.service';
import { RestApiService } from './rest-api.service';
import { IMangaApi } from './imanga-api';
import { Manga } from '../../entities/manga';
import { Observable } from 'rxjs';
import { Anime } from '../../entities/anime';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MangaApiService extends IMangaApi {

  private readonly MANGA_URL = 'manga';

  constructor( private http: HttpClientService, private restApiService: RestApiService ) {
    super();
  }

  getMangas(): Observable<Manga[]> {
    return this.http.get<Manga[]>({
      url: this.getUrl()
    })
      .pipe(
        catchError(this.restApiService.handleError('getMangas()', []))
      );
  }

  getManga( id: number ): Observable<Manga> {
    return this.http.get<Manga>({
      url: this.getUrl(id)
    })
      .pipe(
        catchError(this.restApiService.handleError(`getManga(${id})`, null))
      );
  }

  getImage( manga: Manga ): string {
    return this.getImageUrl(manga.mangaUpdatesId);
  }

  add( manga: Manga ): Observable<Manga> {
    return this.http.post<Manga>({
      url: this.getUrl(),
      body: manga
    })
      .pipe(
        catchError(this.restApiService.handleError(`MangaApi.add(${manga})`, null))
      );
  }

  update( manga: Manga ): Observable<Manga> {
    console.log(manga);
    return this.http.put<Manga>({
      url: this.getUrl(),
      body: manga
    })
      .pipe(
        catchError(this.restApiService.handleError(`MangaApi.update(${manga.id})`, null))
      );
  }

  delete( manga: Manga ): Observable<Manga> {
    return this.http.delete<Manga>({
      url: this.getUrl(manga.id)
    })
      .pipe(
        catchError(this.restApiService.handleError(`MangaApi.delete(${manga.id})`, null))
      );
  }

  private getBaseUrl(): string {
    return `${this.restApiService.url}${this.MANGA_URL}`;
  }

  private getUrl( id: number = 0 ): string {
    return `${this.getBaseUrl()}` + (id !== 0 ? `/${id}` : '');
  }

  private getImageUrl( mangaUpdatesId: string ): string {
    return `${this.getBaseUrl()}/file/` + mangaUpdatesId;
  }
}
