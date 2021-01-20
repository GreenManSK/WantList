import { TestBed } from '@angular/core/testing';

import { AnidbAnimeService } from './anidb-anime.service';

describe('AnidbAnimeServiceService', () => {
  let service: AnidbAnimeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AnidbAnimeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
