import { Inject, Injectable } from '@angular/core';
import { IAnidbAnimeApi } from './api/ianidb-anime-api';
import { Observable } from 'rxjs';
import { AnidbAnime } from '../entities/anidb-anime';

@Injectable({
  providedIn: 'root'
})
export class AnidbAnimeService {

  constructor(@Inject(IAnidbAnimeApi) private anidbAnimeApi: IAnidbAnimeApi) { }

  public getAnidbAnime(): Observable<AnidbAnime[]> {
    return this.anidbAnimeApi.getAnidbAnime();
  }

  public getUrl(id: number): string {
    return `https://anidb.net/anime/${id}`;
  }
}
