import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { AnidbAnimeService } from '../../services/anidb-anime.service';
import { AnidbAnime } from '../../entities/anidb-anime';

@Component({
  selector: 'app-anidb-suggestion',
  templateUrl: './anidb-suggestion.component.html',
  styleUrls: ['./anidb-suggestion.component.sass']
})
export class AnidbSuggestionComponent implements OnInit, OnChanges {

  @Output() onSelect: EventEmitter<AnidbAnime> = new EventEmitter();
  @Input() anidbId: number;
  @Input() name: string;

  public suggestions: AnidbAnime[] = [];

  private animes: AnidbAnime[] = [];
  private animesByNames: object = {};

  constructor( public anidbAnimeService: AnidbAnimeService ) {
  }

  ngOnInit(): void {
    this.anidbAnimeService.getAnidbAnime().subscribe(animes => {
      this.animes = animes;
      for (const anime of animes) {
        if (anime.english) {
          this.animesByNames[anime.english.toLowerCase()] = anime;
        }
        if (anime.japanese) {
          this.animesByNames[anime.japanese.toLowerCase()] = anime;
        }
      }
    });
  }

  ngOnChanges( changes: SimpleChanges ): void {
    this.updateSuggestions();
  }

  public select(anime: AnidbAnime): boolean {
    this.onSelect.emit(anime);
    return false;
  }

  private updateSuggestions() {
    if (this.anidbId) {
      this.suggestions = this.animes.filter(a => a.anidbId === this.anidbId);
    } else if (this.name && !this.name.match(/^\s*$/)) {
      const name = this.name.toLowerCase();
      this.suggestions = Object.keys(this.animesByNames).filter(n => n.startsWith(name)).map(n => this.animesByNames[n]);
    } else {
      this.suggestions = [];
    }
  }

  stopEvent( $event: MouseEvent ) {
    $event.stopPropagation();
  }
}
