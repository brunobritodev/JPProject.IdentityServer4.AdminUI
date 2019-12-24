/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from "@angular/core/testing";
import { SettingsService } from "./settings.service";
import { HttpClientModule } from "@angular/common/http";
import { OAuthModule } from 'angular-oauth2-oidc';
import { VersionService } from '@shared/services/version.service';

describe("Service: Settings", () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientModule, OAuthModule.forRoot()],
      providers: [SettingsService, VersionService]
    });
  });

  it("should ...", inject([SettingsService], (service: SettingsService) => {
    expect(service).toBeTruthy();
  }));
});
