import { TestBed } from '@angular/core/testing';

import { ListOfxService } from './list-ofx.service';

describe('ListOfxService', () => {
  let service: ListOfxService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ListOfxService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
