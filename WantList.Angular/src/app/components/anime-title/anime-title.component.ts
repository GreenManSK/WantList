import { Component, Input, OnInit } from '@angular/core';
import { AnidbAnimeService } from '../../services/anidb-anime.service';
import { Anime } from '../../entities/anime';
import { faRetweet } from '@fortawesome/free-solid-svg-icons';
import { NyaaServiceService } from '../../services/nyaa-service.service';

@Component({
  selector: 'app-anime-title',
  templateUrl: './anime-title.component.html',
  styleUrls: ['./anime-title.component.sass']
})
export class AnimeTitleComponent implements OnInit {
  public readonly qualityString = ['720p', '1080p', 'Any'];

  @Input() anime: Anime;

  public redownloadIcon = faRetweet;
  public isOpen: boolean;
  public anidbUrl: string;
  public nyaaUrl: string;
  public nyaaSearchText: string;

  constructor( private anidbAnimeService: AnidbAnimeService, private nyaaServiceService: NyaaServiceService ) {
  }

  ngOnInit(): void {
    this.isOpen = false;
    this.anidbUrl = this.anidbAnimeService.getUrl(this.anime.anidbId);
    this.nyaaUrl = this.nyaaServiceService.getSearchUrl(this.anime);
    this.nyaaSearchText = this.nyaaServiceService.generateSearchText(this.anime);
  }

  toggle(): boolean {
    this.isOpen = !this.isOpen;
    return false;
  }

}
