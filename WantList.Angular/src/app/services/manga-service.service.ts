import { Inject, Injectable } from '@angular/core';
import { Observable, Subscriber } from 'rxjs';
import { Manga } from '../entities/manga';
import { IMangaApi } from './api/imanga-api';

@Injectable({
  providedIn: 'root'
})
export class MangaServiceService {

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

  public add( manga: Manga ): void {
    this.mangaApi.add(manga).subscribe(addedmanga => {
      if (addedmanga != null) {
        this.manga.push(addedmanga);
        this.updateManga();
      }
    });
  }

  public edit( manga: Manga ): void {
    this.mangaApi.update(manga).subscribe(updatedmanga => {
      if (updatedmanga != null) {
        const index = this.manga.findIndex(a => a.id === updatedmanga.id);
        this.manga[index] = updatedmanga;
        this.updateManga();
      }
    });
  }

  public delete( manga: Manga ): void {
    this.mangaApi.delete(manga).subscribe(updatedmanga => {
      if (updatedmanga != null) {
        const index = this.manga.findIndex(a => a.id === updatedmanga.id);
        this.manga.slice(index, 1);
        this.updateManga();
      }
    });
  }

  private updateManga(): void {
    this.subscriber.next(this.manga);
  }
}
