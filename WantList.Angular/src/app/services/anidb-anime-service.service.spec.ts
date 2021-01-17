import { TestBed } from '@angular/core/testing';

import { AnidbAnimeServiceService } from './anidb-anime-service.service';

describe('AnidbAnimeServiceService', () => {
  let service: AnidbAnimeServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AnidbAnimeServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
