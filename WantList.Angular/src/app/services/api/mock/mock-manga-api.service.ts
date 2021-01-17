import { Injectable } from '@angular/core';
import { IMangaApi } from '../imanga-api';
import { Manga } from '../../../entities/manga';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MockMangaApiService implements IMangaApi {

  private mangas: Manga[];

  constructor() {
    this.mangas = [];
    for (let i = 0; i < 100; i++) {
      this.mangas.push(this.randomManga());
    }
  }

  private randomManga(): Manga {
    const manga = new Manga();
    manga.id = this.randomNum(1, 1000);
    manga.name = this.randomNum(1000000, 99999999).toString();
    manga.mangaUpdatesId = this.randomNum(1, 1000);
    manga.addedDateTime = this.randomDate(new Date(1, 1, 1980), new Date());
    manga.wantRank = this.randomNum(1, 10);
    manga.completed = this.randomNum(1, 100) % 2 === 0;
    return manga;
  }

  randomNum( min: number, max: number ): number {
    return Math.random() * (max - min) + min;
  }

  randomDate( start: Date, end: Date ) {
    return new Date(start.getTime() + Math.random() * (end.getTime() - start.getTime()));
  }

  add( manga: Manga ): Observable<Manga> {
    return new Observable<Manga>(r => {
      this.mangas.push(manga);
      r.next(manga);
    });
  }

  delete( manga: Manga ): Observable<Manga> {
    return new Observable<Manga>(r => {
      const index = this.mangas.indexOf(manga);
      if (index >= 0) {
        this.mangas.slice(index, 1);
      }
      r.next(manga);
    });
  }

  getAnimeImage( manga: Manga ): string {
    return 'assets/test.jpg';
  }

  getManga( id: number ): Observable<Manga> {
    return new Observable<Manga>(r => r.next(this.mangas.filter(a => a.id === id)[0]));
  }

  getMangas(): Observable<Manga[]> {
    return new Observable<Manga[]>(r => r.next(this.mangas));
  }

  update( manga: Manga ): Observable<Manga> {
    return new Observable<Manga>(r => r.next(manga));
  }
}
