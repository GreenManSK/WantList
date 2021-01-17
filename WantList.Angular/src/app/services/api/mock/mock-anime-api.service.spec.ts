import { TestBed } from '@angular/core/testing';

import { MockAnimeApiService } from './mock-anime-api.service';

describe('MockAnimeApiService', () => {
  let service: MockAnimeApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MockAnimeApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
