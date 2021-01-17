import { Observable } from 'rxjs';
import { Manga } from '../../entities/manga';

export interface IMangaApi {
  getMangas(): Observable<Manga[]>;
  getManga(id: number): Observable<Manga>;
  add(manga: Manga): Observable<Manga>;
  update(manga: Manga): Observable<Manga>;
  delete(manga: Manga): Observable<Manga>;
  getAnimeImage(manga: Manga): string;
}
