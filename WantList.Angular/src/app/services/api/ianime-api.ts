import { Observable } from 'rxjs';
import { Anime } from '../../entities/anime';

export abstract class IAnimeApi {
  abstract getAnimes(): Observable<Anime[]>;
  abstract getAnime(id: number): Observable<Anime>;
  abstract add(anime: Anime): Observable<Anime>;
  abstract update(anime: Anime): Observable<Anime>;
  abstract delete(anime: Anime): Observable<Anime>;
  abstract getAnimeImage(anime: Anime): string;
}
