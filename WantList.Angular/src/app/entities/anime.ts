import { AnimeType } from './anime-type.enum';
import { Quality } from './quality.enum';

export class Anime {
  id: number;
  name: string;
  anidbId: number;
  addedDateTime: Date;
  releaseDate: Date;
  type: AnimeType;
  episodeCount: number;
  wantRank: number;
  redownload: boolean;
  bluRay: boolean;
  quality: Quality;
  bluRayRelease: string;
}
