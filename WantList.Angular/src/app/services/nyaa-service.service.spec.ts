import { TestBed } from '@angular/core/testing';

import { NyaaServiceService } from './nyaa-service.service';

describe('NyaaServiceService', () => {
  let service: NyaaServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(NyaaServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
