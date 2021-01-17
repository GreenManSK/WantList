import { Injectable } from '@angular/core';
import { IAnidbAnimeApi } from '../ianidb-anime-api';
import { Observable } from 'rxjs';
import { AnidbAnime } from '../../../entities/anidb-anime';

@Injectable({
  providedIn: 'root'
})
export class MockAnidbAnimeApiService implements IAnidbAnimeApi {

  constructor() {
  }

  getAnidbAnime(): Observable<AnidbAnime[]> {
    return new Observable<AnidbAnime[]>(r => {
      r.next([
        new AnidbAnime(10, 'naruto', ''),
        new AnidbAnime(11, '', 'blíČ'),
        new AnidbAnime(13, 'ft', 'fairy téru'),
      ]);
    });
  }

}
