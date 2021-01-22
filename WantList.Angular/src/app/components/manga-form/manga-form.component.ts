import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { MangaService } from '../../services/manga.service';
import { Manga } from '../../entities/manga';

@Component({
  selector: 'app-manga-form',
  templateUrl: './manga-form.component.html',
  styleUrls: ['./manga-form.component.sass']
})
export class MangaFormComponent implements OnInit, OnChanges {

  @Output() onClose: EventEmitter<void> = new EventEmitter();
  @Input() manga: Manga = null;

  public isAdd = false;
  public readonly ranks = [...Array(10).keys()].map(n => n + 1).reverse();
  public editedManga: Manga = null;

  constructor( private mangaService: MangaService ) {
  }

  ngOnInit(): void {
    this.prepareData();
  }

  ngOnChanges( changes: SimpleChanges ): void {
    this.prepareData();
  }

  public prepareData(): void {
    this.isAdd = !!!this.manga.id;
    this.editedManga = new Manga();
    if (this.isAdd) {
      this.manga.wantRank = 7;
    }
    Manga.copyFrom(this.manga, this.editedManga);
  }

  public onSubmit(): boolean {
    if (this.isAdd) {
      this.mangaService.add(this.editedManga, (m) => this.onSuccess(m));
    } else {
      this.mangaService.edit(this.editedManga, (m) => this.onSuccess(m));
    }
    return false;
  }

  public onSuccess(manga: Manga): void {
    Manga.copyFrom(manga, this.manga);
    this.close();
  }

  public close(): boolean {
    this.onClose.emit();
    return false;
  }

}
