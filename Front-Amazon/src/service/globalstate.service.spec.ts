import { TestBed } from '@angular/core/testing';

import { GlobalstateService } from './globalstate.service';

describe('GlobalstateService', () => {
  let service: GlobalstateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GlobalstateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});


