/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from "@angular/core/testing";
import { FooterComponent } from "./footer.component";

import { SettingsService } from "@core/settings/settings.service";
import { HttpClientModule } from "@angular/common/http";
import { OAuthModule } from 'angular-oauth2-oidc';
import { VersionService } from '@shared/services/version.service';

describe("Component: Footer", () => {

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientModule, OAuthModule.forRoot()],
            providers: [SettingsService, VersionService]
        }).compileComponents();
    });

    it("should create an instance", async(inject([SettingsService], (settingsService) => {
        let component = new FooterComponent(settingsService);
        expect(component).toBeTruthy();
    })));
});
