import { Component, OnInit } from '@angular/core';
import { AnimeService } from '../../services/anime.service';
import { Anime } from '../../entities/anime';
import { Column } from '../list-table/Column';
import { faEdit, faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { AnimeType } from '../../entities/anime-type.enum';
import { AnimeFilter } from '../../data/anime-filter';

@Component({
  selector: 'app-anime',
  templateUrl: './anime.component.html',
  styleUrls: ['./anime.component.sass']
})
export class AnimeComponent implements OnInit {

  public addIcon = faPlus;
  public editIcon = faEdit;
  public deleteIcon = faTrash;

  public anime: Anime[] = [];
  public columns = [
    new Column('image', '', false, 'image'),
    new Column('name', 'Title', true, 'left'),
    new Column('type', 'Type', true),
    new Column('wantRank', 'Rank', true),
    new Column('addedDateTime', 'Added', true),
    new Column('releaseDate', 'Released', true),
    new Column('episodeCount', 'Episodes', true),
    new Column('icons', '', false, 'icons'),
  ];
  public filter: AnimeFilter;
  public openForm = false;
  public activeAnime: Anime = new Anime();

  public clearAnime: Anime[] = [];
  public images: string[] = [];
  public ids: string[] = [];

  constructor( private animeService: AnimeService ) {
    this.filter = new AnimeFilter();
  }

  ngOnInit(): void {
    this.animeService.getAnime().subscribe(anime => {
      this.clearAnime = [...anime];
      this.refilter();
    });
  }

  public add(): boolean {
    this.activeAnime = new Anime();
    this.openForm = true;
    return true;
  }

  public edit( anime: Anime ): boolean {
    this.activeAnime = anime;
    this.openForm = true;
    return true;
  }

  public delete( anime: Anime ): boolean {
    if (confirm(`Do you want to delete this anime ${anime.name}?`)) {
      this.animeService.delete(anime);
    }
    return false;
  }

  public typeToString( type: AnimeType ): string {
    switch (type) {
      case AnimeType.Movie:
        return 'Movie';
      case AnimeType.OVA:
        return 'OVA';
      case AnimeType.P18:
        return '+18';
      case AnimeType.Series:
        return 'Series';
      default:
        return 'Unknown';
    }
  }

  public refilter(): void {
    this.anime = this.filter.filter(this.clearAnime.filter(a => !a.deleted));
    this.prepareData();
  }

  public prepareData(): void {
    this.ids = [];
    this.images = [];
    for (const anime of this.anime) {
      this.ids[anime.id] = this.getAnimeId(anime);
      this.images[anime.id] = this.animeService.getAnimeImage(anime);
    }
  }

  public closeForm(): void {
    this.openForm = false;
    this.activeAnime = null;
  }

  public getAnimeId( anime: Anime ): string {
    return `anime${anime.id}`;
  }
}
