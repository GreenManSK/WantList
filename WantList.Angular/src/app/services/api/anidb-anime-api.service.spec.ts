import { TestBed } from '@angular/core/testing';

import { AnidbAnimeApiService } from './anidb-anime-api.service';

describe('AnidbAnimeApiService', () => {
  let service: AnidbAnimeApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AnidbAnimeApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
