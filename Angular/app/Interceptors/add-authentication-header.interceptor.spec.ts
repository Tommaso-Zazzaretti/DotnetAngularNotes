import { TestBed } from '@angular/core/testing';

import { AddAuthenticationHeaderInterceptor } from './add-authentication-header.interceptor';

describe('AddAuthenticationHeaderInterceptor', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      AddAuthenticationHeaderInterceptor
      ]
  }));

  it('should be created', () => {
    const interceptor: AddAuthenticationHeaderInterceptor = TestBed.inject(AddAuthenticationHeaderInterceptor);
    expect(interceptor).toBeTruthy();
  });
});
