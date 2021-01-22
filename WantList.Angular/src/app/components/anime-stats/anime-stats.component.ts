import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Anime } from '../../entities/anime';
import { AnimeType } from '../../entities/anime-type.enum';

@Component({
  selector: 'app-anime-stats',
  templateUrl: './anime-stats.component.html',
  styleUrls: ['./anime-stats.component.sass']
})
export class AnimeStatsComponent implements OnInit, OnChanges {

  @Input() anime: Anime[] = [];

  public all = 0;
  public series = 0;
  public movies = 0;
  public p18s = 0;
  public ovas = 0;
  public redownload = 0;
  public deleted = 0;

  constructor() {
  }

  ngOnInit(): void {
    this.computeStats();
  }

  ngOnChanges( changes: SimpleChanges ): void {
    this.computeStats();
  }

  private computeStats(): void {
    for (const a of this.anime) {
      if (a.deleted) {
        this.deleted++;
        continue;
      }
      if (a.redownload) {
        this.redownload++;
      } else {
        this.all++;
        switch (a.type) {
          case AnimeType.Series:
            this.series++;
            break;
          case AnimeType.P18:
            this.p18s++;
            break;
          case AnimeType.OVA:
            this.ovas++;
            break;
          case AnimeType.Movie:
            this.movies++;
            break;
        }
      }
    }
  }

}
