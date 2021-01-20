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

  constructor( public anidbAnimeService: AnidbAnimeService ) {
  }

  ngOnInit(): void {
    this.anidbAnimeService.getAnidbAnime().subscribe(animes => this.animes = animes);
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
      this.suggestions = this.animes.filter(a => a.english.startsWith(this.name) || a.japanese.startsWith(this.name));
    } else {
      this.suggestions = [];
    }
  }

  stopEvent( $event: MouseEvent ) {
    $event.stopPropagation();
  }
}
