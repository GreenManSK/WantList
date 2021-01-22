import { Component, OnInit } from '@angular/core';
import { faEdit, faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { Manga } from '../../entities/manga';
import { Column } from '../list-table/Column';
import { MangaService } from '../../services/manga.service';

@Component({
  selector: 'app-manga',
  templateUrl: './manga.component.html',
  styleUrls: ['./manga.component.sass']
})
export class MangaComponent implements OnInit {
  public addIcon = faPlus;
  public editIcon = faEdit;
  public deleteIcon = faTrash;

  public manga: Manga[] = [];
  public columns = [
    new Column('image', '', false, 'image'),
    new Column('name', 'Title', true, 'left'),
    new Column('wantRank', 'Rank', true),
    new Column('addedDateTime', 'Added', true),
    new Column('completed', 'Completed', true),
    new Column('icons', '', false, 'icons'),
  ];

  public openForm = false;
  public activeManga: Manga = new Manga();

  constructor( public mangaService: MangaService ) {
  }

  ngOnInit(): void {
    this.mangaService.getManga().subscribe(manga => {
      this.manga = manga;
    });
  }

  public add(): boolean {
    this.activeManga = new Manga();
    this.openForm = true;
    return true;
  }

  public edit( manga: Manga ): boolean {
    this.activeManga = manga;
    this.openForm = true;
    return true;
  }

  public delete( manga: Manga ): boolean {
    if (confirm(`Do you want to delete this manga ${manga.name}?`)) {
      this.mangaService.delete(manga);
    }
    return false;
  }

  public closeForm(): void {
    this.openForm = false;
    this.activeManga = null;
  }

  public getMangaId( manga: Manga ): string {
    return `manga${manga.id}`;
  }
}
