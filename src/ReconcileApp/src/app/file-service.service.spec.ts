import { TestBed } from '@angular/core/testing';

import { FileServiceService } from './file-service.service';

describe('FileServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FileServiceService = TestBed.get(FileServiceService);
    expect(service).toBeTruthy();
  });
});
