import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { Manga } from '../../entities/manga';

@Component({
  selector: 'app-manga-stats',
  templateUrl: './manga-stats.component.html',
  styleUrls: ['./manga-stats.component.sass']
})
export class MangaStatsComponent implements OnInit, OnChanges {

  @Input() manga: Manga[];

  public all = 0;
  public completed = 0;
  public running = 0;
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
    this.all = this.completed = this.running = this.deleted = 0;
    for (const m of this.manga) {
      if (m.deleted) {
        this.deleted++;
        continue;
      }
      this.all++;
      if (m.completed) {
        this.completed++;
      } else {
        this.running++;
      }
    }
  }
}
