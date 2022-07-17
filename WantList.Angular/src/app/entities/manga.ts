export class Manga {
  id: number;
  name: string;
  mangaUpdatesId: string;
  addedDateTime: Date;
  wantRank: number;
  missingVolumes: string;
  completed: boolean;
  deleted: boolean;

  public static copyFrom( from: Manga, to: Manga ): void {
    Object.keys(from).forEach(key => to[key] = from[key]);
  }
}
