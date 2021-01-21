import { Component, Input, OnInit } from '@angular/core';
import { Manga } from '../../entities/manga';
import { MangaUpdatesService } from '../../services/manga-updates.service';

@Component({
  selector: 'app-manga-title',
  templateUrl: './manga-title.component.html',
  styleUrls: ['./manga-title.component.sass']
})
export class MangaTitleComponent implements OnInit {

  @Input() manga: Manga;

  public isOpen: boolean;

  constructor(public mangaUpdatesService: MangaUpdatesService) {
    this.isOpen = false;
  }

  ngOnInit(): void {
  }

  toggle(): boolean {
    this.isOpen = !this.isOpen;
    return false;
  }

}
