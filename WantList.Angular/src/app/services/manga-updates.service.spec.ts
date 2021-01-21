import { TestBed } from '@angular/core/testing';

import { MangaUpdatesService } from './manga-updates.service';

describe('MangaUpdatesService', () => {
  let service: MangaUpdatesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MangaUpdatesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
