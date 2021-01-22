import { Injectable } from '@angular/core';
import { RestApiService } from './rest-api.service';
import { IAnidbAnimeApi } from './ianidb-anime-api';
import { Observable } from 'rxjs';
import { AnidbAnime } from '../../entities/anidb-anime';
import { HttpClientService } from './http-client.service';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AnidbAnimeApiService extends IAnidbAnimeApi {

  private readonly ANIDB_ANIME_URL = 'anidbanime';

  constructor( private http: HttpClientService, private restApiService: RestApiService ) {
    super();
  }

  getAnidbAnime(): Observable<AnidbAnime[]> {
    return this.http.get<AnidbAnime[]>({
      url: this.getUrl()
    })
      .pipe(
        catchError(this.restApiService.handleError('getAnidbAnime()', []))
      );
  }


  private getUrl(): string {
    return `${this.restApiService.url}${this.ANIDB_ANIME_URL}`;
  }
}
