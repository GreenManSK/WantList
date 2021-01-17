import { Anime } from '../entities/anime';
import { AnimeType } from '../entities/anime-type.enum';

export class AnimeFilter {
  public types = {
    movie: true,
    series: true,
    ova: true,
    p18: true
  };
  public age = {
    redownload: true,
    new: true
  };

  constructor() {
  }

  public filter( anime: Anime[] ): Anime[] {
    return anime.filter(this.animeFilter.bind(this));
  }

  private animeFilter( anime: Anime ): boolean {
    if (anime.type === AnimeType.Movie && !this.types.movie) {
      return false;
    }
    if (anime.type === AnimeType.Series && !this.types.series) {
      return false;
    }
    if (anime.type === AnimeType.OVA && !this.types.ova) {
      return false;
    }
    if (anime.type === AnimeType.P18 && !this.types.p18) {
      return false;
    }
    if (anime.redownload && !this.age.redownload) {
      return false;
    }
    if (!anime.redownload && !this.age.new) {
      return false;
    }
    return true;
  }
}
