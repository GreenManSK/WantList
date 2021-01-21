import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { Anime } from '../../entities/anime';
import { AnimeService } from '../../services/anime.service';
import { AnimeType } from '../../entities/anime-type.enum';
import { Quality } from '../../entities/quality.enum';
import { AnidbAnime } from '../../entities/anidb-anime';

@Component({
  selector: 'app-anime-form',
  templateUrl: './anime-form.component.html',
  styleUrls: ['./anime-form.component.sass']
})
export class AnimeFormComponent implements OnInit, OnChanges {

  @Output() onClose: EventEmitter<void> = new EventEmitter();
  @Input() anime: Anime = null;

  public editedAnime: Anime = null;

  public isAdd = false;
  public readonly animeTypes = [
    'Series',
    'Movie',
    'OVA',
    '+18'
  ];
  public readonly ranks = [...Array(10).keys()].map(n => n + 1).reverse();
  public readonly quality = [
    '720p',
    '1080p',
    'Any'
  ];

  constructor( public animeService: AnimeService ) {
  }

  ngOnInit(): void {
    this.prepareData();
  }

  ngOnChanges( changes: SimpleChanges ) {
    this.prepareData();
  }

  private prepareData(): void {
    this.isAdd = !!!this.anime.id;
    this.editedAnime = new Anime();
    if (this.isAdd) {
      this.anime.type = AnimeType.Series;
      this.anime.wantRank = 7;
      this.anime.quality = Quality.p720;
    }
    this.editedAnime.copyFrom(this.anime);
  }

  public onSubmit(): boolean {
    if (this.isAdd) {
      this.animeService.add(this.anime, () => this.onSuccess());
    } else {
      this.animeService.edit(this.anime, () => this.onSuccess());
    }
    return false;
  }

  public onSuccess(): void {
    this.anime.copyFrom(this.editedAnime);
    this.close();
  }

  public close(): boolean {
    this.onClose.emit();
    return false;
  }

  public onSuggestion( anime: AnidbAnime ): void {
    if (!this.anime.anidbId) {
      this.anime.anidbId = anime.anidbId;
    } else {
      this.anime.name = anime.japanese ? anime.japanese : anime.english;
    }
  }
}
