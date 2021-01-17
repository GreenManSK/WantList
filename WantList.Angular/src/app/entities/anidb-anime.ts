export class AnidbAnime {
  anidbId: number;
  japanese: string;
  english: string;


  constructor( anidbId: number, japanese: string, english: string ) {
    this.anidbId = anidbId;
    this.japanese = japanese;
    this.english = english;
  }
}
