import { TestBed, inject } from '@angular/core/testing';

import { DisabilityGroupService } from './disability-group.service';

describe('DisabilityGroupService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DisabilityGroupService]
    });
  });

  it('should be created', inject([DisabilityGroupService], (service: DisabilityGroupService) => {
    expect(service).toBeTruthy();
  }));
});
