import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AnimeComponent } from './components/anime/anime.component';
import { MangaComponent } from './components/manga/manga.component';


const routes: Routes = [
  { path: '', component: AnimeComponent },
  { path: 'manga', component: MangaComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
