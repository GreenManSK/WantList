import { Injectable } from '@angular/core';
import { IAnimeApi } from '../ianime-api';
import { Observable } from 'rxjs';
import { Anime } from '../../../entities/anime';

@Injectable({
  providedIn: 'root'
})
export class MockAnimeApiService implements IAnimeApi {

  private animes: Anime[];

  constructor() {
    this.animes = [];
    for (let i = 0; i < 100; i++) {
      this.animes.push(this.randomAnime());
    }
  }

  randomAnime(): Anime {
    const anime = new Anime();
    anime.id = this.randomNum(1, 1000);
    anime.name = this.randomNum(1000000, 99999999).toString();
    anime.anidbId = this.randomNum(1, 1000);
    anime.addedDateTime = this.randomDate(new Date(1, 1, 1980), new Date());
    anime.releaseDate = this.randomDate(new Date(1, 1, 2015), new Date());
    anime.episodeCount = this.randomNum(1, 100);
    anime.wantRank = this.randomNum(1, 10);
    anime.redownload = this.randomNum(1, 100) % 2 === 0;
    anime.bluRay = this.randomNum(1, 100) % 2 === 0;
    anime.quality = this.randomNum(0, 2);
    anime.bluRayRelease = this.randomNum(1000000, 99999999).toString();
    return anime;
  }

  randomNum( min: number, max: number ): number {
    return Math.random() * (max - min) + min;
  }

  randomDate( start: Date, end: Date ) {
    return new Date(start.getTime() + Math.random() * (end.getTime() - start.getTime()));
  }

  getAnime( id: number ): Observable<Anime> {
    return new Observable<Anime>(r => r.next(this.animes.filter(a => a.id === id)[0]));
  }

  getAnimeImage( anime: Anime ): string {
    return 'assets/test.jpg';
  }

  getAnimes(): Observable<Anime[]> {
    return new Observable<Anime[]>(r => r.next(this.animes));
  }

  add( anime: Anime ): Observable<Anime> {
    return new Observable<Anime>(r => {
      this.animes.push(anime);
      r.next(anime);
    });
  }

  update( anime: Anime ): Observable<Anime> {
    return new Observable<Anime>(r => {
      r.next(anime);
    });
  }

  delete( anime: Anime ): Observable<Anime> {
    return new Observable<Anime>(r => {
      const index = this.animes.indexOf(anime);
      if (index >= 0) {
        this.animes.slice(index);
      }
      r.next(anime);
    });
  }


}
