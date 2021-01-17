import { Observable } from 'rxjs';
import { Anime } from '../../entities/anime';

export interface IAnimeApi {
  getAnimes(): Observable<Anime[]>;
  getAnime(id: number): Observable<Anime>;
  add(anime: Anime): Observable<Anime>;
  update(anime: Anime): Observable<Anime>;
  delete(anime: Anime): Observable<Anime>;
  getAnimeImage(anime: Anime): string;
}
