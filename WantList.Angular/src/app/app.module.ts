import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { IAnimeApi } from './services/api/ianime-api';
import { MockAnimeApiService } from './services/api/mock/mock-anime-api.service';
import { MockMangaApiService } from './services/api/mock/mock-manga-api.service';
import { IMangaApi } from './services/api/imanga-api';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FontAwesomeModule
  ],
  providers: [
    {provide: IAnimeApi, useClass: MockAnimeApiService},
    {provide: IMangaApi, useClass: MockMangaApiService}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
