import { Observable } from 'rxjs';
import { Manga } from '../../entities/manga';

export abstract class IMangaApi {
  abstract getMangas(): Observable<Manga[]>;
  abstract getManga(id: number): Observable<Manga>;
  abstract add(manga: Manga): Observable<Manga>;
  abstract update(manga: Manga): Observable<Manga>;
  abstract delete(manga: Manga): Observable<Manga>;
  abstract getImage(manga: Manga): string;
}
