export class Manga {
  id: number;
  name: string;
  mangaUpdatesId: number;
  addedDateTime: Date;
  wantRank: number;
  missingVolumes: string;
  completed: boolean;

  public copyFrom( manga: Manga ): void {
    Object.keys(manga).forEach(key => this[key] = manga[key]);
  }
}
