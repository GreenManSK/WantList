import { Injectable } from '@angular/core';
import { Anime } from '../entities/anime';
import { Quality } from '../entities/quality.enum';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NyaaServiceService {

  private nyaaDomain: string;

  constructor() {
    this.nyaaDomain = environment.nyaaDomain;
  }

  public getSearchUrl( anime: Anime ): string {
    const searchText = this.generateSearchText(anime);
    return this.nyaaDomain + '/?f=0&c=0_0&q=' + encodeURIComponent(searchText).replace(/%20/g, '+');
  }

  public generateSearchText( anime: Anime ): string {
    let text = anime.name;
    text += anime.bluRay ? ' BD' : '';
    if (anime.quality !== Quality.any) {
      text += anime.quality === Quality.p1080 ? ' 1080' : ' 720';
    }
    return text;
  }
}
