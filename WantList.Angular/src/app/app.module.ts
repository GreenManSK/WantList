import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { IAnimeApi } from './services/api/ianime-api';
import { MockAnimeApiService } from './services/api/mock/mock-anime-api.service';
import { MockMangaApiService } from './services/api/mock/mock-manga-api.service';
import { IMangaApi } from './services/api/imanga-api';
import { AnimeComponent } from './components/anime/anime.component';
import { MangaComponent } from './components/manga/manga.component';
import { ListTableComponent } from './components/list-table/list-table.component';
import { AnimeTitleComponent } from './components/anime-title/anime-title.component';
import { FormsModule } from '@angular/forms';
import { AnimeFormComponent } from './components/anime-form/anime-form.component';
import { IAnidbAnimeApi } from './services/api/ianidb-anime-api';
import { MockAnidbAnimeApiService } from './services/api/mock/mock-anidb-anime-api.service';
import { AnidbSuggestionComponent } from './components/anidb-suggestion/anidb-suggestion.component';
import { RandomButtonComponent } from './components/random-button/random-button.component';
import { AnimeStatsComponent } from './components/anime-stats/anime-stats.component';
import { MangaTitleComponent } from './components/manga-title/manga-title.component';
import { APP_BASE_HREF } from '@angular/common';
import { MangaFormComponent } from './manga-form/manga-form.component';

@NgModule({
  declarations: [
    AppComponent,
    AnimeComponent,
    MangaComponent,
    ListTableComponent,
    AnimeTitleComponent,
    AnimeFormComponent,
    AnidbSuggestionComponent,
    RandomButtonComponent,
    AnimeStatsComponent,
    MangaTitleComponent,
    MangaFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule,
    FormsModule
  ],
  providers: [
    {provide: APP_BASE_HREF, useValue: '/'},
    {provide: IAnimeApi, useClass: MockAnimeApiService},
    {provide: IMangaApi, useClass: MockMangaApiService},
    {provide: IAnidbAnimeApi, useClass: MockAnidbAnimeApiService},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
