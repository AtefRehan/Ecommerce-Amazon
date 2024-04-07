import { TestBed } from '@angular/core/testing';

import { PurchaseditemsService } from './purchaseditems.service';

describe('PurchaseditemsService', () => {
  let service: PurchaseditemsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PurchaseditemsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});


