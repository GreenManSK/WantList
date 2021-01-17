import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AnidbAnimeServiceService {

  constructor() { }
  // TODO: when implementing whispering name

  public getUrl(id: number): string {
    return `https://anidb.net/anime/${id}`;
  }
}
