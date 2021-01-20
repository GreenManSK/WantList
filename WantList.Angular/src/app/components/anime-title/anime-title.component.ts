import { Component, Input, OnInit } from '@angular/core';
import { AnidbAnimeService } from '../../services/anidb-anime.service';
import { Quality } from '../../entities/quality.enum';
import { Anime } from '../../entities/anime';
import { faRetweet } from '@fortawesome/free-solid-svg-icons';
import { NyaaServiceService } from '../../services/nyaa-service.service';

@Component({
  selector: 'app-anime-title',
  templateUrl: './anime-title.component.html',
  styleUrls: ['./anime-title.component.sass']
})
export class AnimeTitleComponent implements OnInit {

  @Input() anime: Anime;

  public redownloadIcon = faRetweet;
  public isOpen: boolean;

  constructor( public anidbAnimeService: AnidbAnimeService, public nyaaServiceService: NyaaServiceService) {
  }

  ngOnInit(): void {
    this.isOpen = false;
  }

  public qualityString( quality: Quality ): string {
    switch (quality) {
      case Quality.p720:
        return '720p';
      case Quality.p1080:
        return '1080p';
      default:
        return 'Any';
    }
  }

  toggle(): boolean {
    this.isOpen = !this.isOpen;
    return false;
  }

}
