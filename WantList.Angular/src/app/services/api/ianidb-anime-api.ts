import { Observable } from 'rxjs';
import { AnidbAnime } from '../../entities/anidb-anime';

export interface IAnidbAnimeApi {
  getAnidbAnime(): Observable<AnidbAnime[]>;
}
