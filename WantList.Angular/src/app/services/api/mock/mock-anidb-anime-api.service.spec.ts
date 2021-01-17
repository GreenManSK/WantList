import { TestBed } from '@angular/core/testing';

import { MockAnidbAnimeApiService } from './mock-anidb-anime-api.service';

describe('MockAnidbAnimeApiService', () => {
  let service: MockAnidbAnimeApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MockAnidbAnimeApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
