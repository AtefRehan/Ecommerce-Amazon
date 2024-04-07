import { TestBed } from '@angular/core/testing';

import { ReversegeoencodingService } from './reversegeoencoding.service';

describe('ReversegeoencodingService', () => {
  let service: ReversegeoencodingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReversegeoencodingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

