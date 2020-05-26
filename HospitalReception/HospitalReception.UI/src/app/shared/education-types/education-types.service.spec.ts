import { TestBed, inject } from '@angular/core/testing';

import { EducationTypesService } from './education-types.service';

describe('EducationTypesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [EducationTypesService]
    });
  });

  it('should be created', inject([EducationTypesService], (service: EducationTypesService) => {
    expect(service).toBeTruthy();
  }));
});
