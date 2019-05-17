import { TestBed } from '@angular/core/testing';

import { TransactionServiceService } from './transaction-service.service';

describe('TransactionServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: TransactionServiceService = TestBed.get(TransactionServiceService);
    expect(service).toBeTruthy();
  });
});
