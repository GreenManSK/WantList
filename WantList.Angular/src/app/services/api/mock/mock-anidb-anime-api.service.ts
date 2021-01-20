import { Injectable } from '@angular/core';
import { IAnidbAnimeApi } from '../ianidb-anime-api';
import { Observable } from 'rxjs';
import { AnidbAnime } from '../../../entities/anidb-anime';

@Injectable({
  providedIn: 'root'
})
export class MockAnidbAnimeApiService extends IAnidbAnimeApi {

  randomAnidbAnime(): AnidbAnime {
    const anidbAnime = new AnidbAnime();
    anidbAnime.anidbId = this.randomNum(1, 1000);
    const type = this.randomNum(1, 100);
    if (type % 3 === 0) {
      anidbAnime.english = this.randomNum(1000000000, 99999999999).toString();
      anidbAnime.japanese = '';
    } else if (type % 3 === 1) {
      anidbAnime.english = '';
      anidbAnime.japanese = this.randomNum(1000000000, 99999999999).toString();
    } else {
      anidbAnime.english = this.randomNum(1000000000, 99999999999).toString();
      anidbAnime.japanese = this.randomNum(1000000000, 99999999999).toString();
    }
    return anidbAnime;
  }

  randomNum( min: number, max: number ): number {
    return Math.round(Math.random() * (max - min) + min);
  }

  getAnidbAnime(): Observable<AnidbAnime[]> {
    const animes = [];
    for (let i = 0; i < 100; i++) {
      animes.push(this.randomAnidbAnime());
    }
    return new Observable<AnidbAnime[]>(r => {
      r.next(animes);
    });
  }

}
