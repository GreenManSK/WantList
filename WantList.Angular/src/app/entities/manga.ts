export class Manga {
  id: number;
  name: string;
  mangaUpdatesId: number;
  addedDateTime: Date;
  wantRank: number;
  missingVolumes: string;
  completed: boolean;

  public static copyFrom( from: Manga, to: Manga ): void {
    Object.keys(from).forEach(key => to[key] = from[key]);
  }
}
