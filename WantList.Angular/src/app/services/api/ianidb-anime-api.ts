import { Observable } from 'rxjs';
import { AnidbAnime } from '../../entities/anidb-anime';

export abstract class IAnidbAnimeApi {
  abstract getAnidbAnime(): Observable<AnidbAnime[]>;
}
