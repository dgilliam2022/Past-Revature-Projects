import { TestBed } from '@angular/core/testing';

import { PostSpiServicesService } from './post-spi-services.service';

describe('PostSpiServicesService', () => {
  let service: PostSpiServicesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PostSpiServicesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
