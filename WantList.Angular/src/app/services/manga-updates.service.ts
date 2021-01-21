import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class MangaUpdatesService {

  constructor() { }

  public getUrl(id: number): string {
    return `https://www.mangaupdates.com/series.html?id=${id}`;
  }
}
