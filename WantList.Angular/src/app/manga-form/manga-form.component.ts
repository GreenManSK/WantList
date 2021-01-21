import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { MangaService } from '../services/manga.service';
import { Manga } from '../entities/manga';

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
    this.editedManga.copyFrom(this.manga);
  }

  public onSubmit(): boolean {
    if (this.isAdd) {
      this.mangaService.add(this.manga, () => this.onSuccess());
    } else {
      this.mangaService.edit(this.manga, () => this.onSuccess());
    }
    return false;
  }

  public onSuccess(): void {
    this.manga.copyFrom(this.editedManga);
    this.close();
  }

  public close(): boolean {
    this.onClose.emit();
    return false;
  }

}
