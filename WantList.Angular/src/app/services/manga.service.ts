import { Inject, Injectable } from '@angular/core';
import { Observable, Subscriber } from 'rxjs';
import { Manga } from '../entities/manga';
import { IMangaApi } from './api/imanga-api';
import { Anime } from '../entities/anime';

@Injectable({
  providedIn: 'root'
})
export class MangaService {

  private manga: Manga[] = [];
  private observable: Observable<Manga[]>;
  private subscriber: Subscriber<Manga[]>;

  constructor( @Inject(IMangaApi) private mangaApi: IMangaApi ) {
    this.observable = new Observable<Manga[]>(r => {
      this.subscriber = r;
      mangaApi.getMangas().subscribe(manga => {
        this.manga = manga;
        this.updateManga();
      });
    });
  }

  public getManga(): Observable<Manga[]> {
    return this.observable;
  }

  public add( manga: Manga, onSuccess: () => void  ): void {
    this.mangaApi.add(manga).subscribe(addedmanga => {
      if (addedmanga != null) {
        this.manga.push(addedmanga);
        this.updateManga();
        onSuccess();
      }
    });
  }

  public edit( manga: Manga, onSuccess: () => void  ): void {
    this.mangaApi.update(manga).subscribe(updatedmanga => {
      if (updatedmanga != null) {
        const index = this.manga.findIndex(a => a.id === updatedmanga.id);
        this.manga[index] = updatedmanga;
        this.updateManga();
        onSuccess();
      }
    });
  }

  public delete( manga: Manga ): void {
    this.mangaApi.delete(manga).subscribe(updatedmanga => {
      if (updatedmanga != null) {
        const index = this.manga.findIndex(a => a.id === updatedmanga.id);
        this.manga.splice(index, 1);
        this.updateManga();
      }
    });
  }

  public getImage(manga: Manga): string {
    return this.mangaApi.getImage(manga);
  }

  private updateManga(): void {
    this.subscriber.next(this.manga);
  }
}
