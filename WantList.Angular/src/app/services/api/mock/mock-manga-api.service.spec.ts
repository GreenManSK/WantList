import { TestBed } from '@angular/core/testing';

import { MockMangaApiService } from './mock-manga-api.service';

describe('MockMangaApiService', () => {
  let service: MockMangaApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MockMangaApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
