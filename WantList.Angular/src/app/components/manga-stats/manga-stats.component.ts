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

  constructor() {
  }


  ngOnInit(): void {
    this.computeStats();
  }

  ngOnChanges( changes: SimpleChanges ): void {
    this.computeStats();
  }

  private computeStats(): void {
    for (const m of this.manga) {
      this.all++;
      if (m.completed) {
        this.completed++;
      } else {
        this.running++;
      }
    }
  }
}