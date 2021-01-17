import { Inject, Injectable } from '@angular/core';
import { IAnimeApi } from './api/ianime-api';
import { Anime } from '../entities/anime';
import { Observable, Subscriber } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AnimeService {

  private anime: Anime[] = [];
  private observable: Observable<Anime[]>;
  private subscriber: Subscriber<Anime[]>;

  constructor(@Inject(IAnimeApi) private animeApi: IAnimeApi ) {
    this.observable = new Observable<Anime[]>(r => {
      this.subscriber = r;
      animeApi.getAnimes().subscribe(animes => {
        this.anime = animes;
        this.updateAnime();
      });
    });
  }

  public getAnime(): Observable<Anime[]> {
    return this.observable;
  }

  public add( anime: Anime ): void {
    this.animeApi.add(anime).subscribe(addedAnime => {
      if (addedAnime != null) {
        this.anime.push(addedAnime);
        this.updateAnime();
      }
    });
  }

  public edit( anime: Anime ): void {
    this.animeApi.update(anime).subscribe(updatedAnime => {
      if (updatedAnime != null) {
        const index = this.anime.findIndex(a => a.id === updatedAnime.id);
        this.anime[index] = updatedAnime;
        this.updateAnime();
      }
    });
  }

  public delete( anime: Anime ): void {
    this.animeApi.delete(anime).subscribe(updatedAnime => {
      if (updatedAnime != null) {
        const index = this.anime.findIndex(a => a.id === updatedAnime.id);
        this.anime.splice(index, 1);
        this.updateAnime();
      }
    });
  }

  public getAnimeImage( anime: Anime ): string {
    return this.animeApi.getAnimeImage(anime);
  }

  private updateAnime(): void {
    this.subscriber.next(this.anime);
  }
}
