import { HttpClient, HttpClientModule } from '@angular/common/http';
import { async, inject, TestBed } from '@angular/core/testing';
import { SettingsService } from '@core/settings/settings.service';
import { ThemesService } from '@core/themes/themes.service';
import { TranslatorService } from '@core/translator/translator.service';
import { TranslateLoader, TranslateModule, TranslateService } from '@ngx-translate/core';
import { VersionService } from '@shared/services/version.service';
import { SharedModule } from '@shared/shared.module';
import { OAuthModule } from 'angular-oauth2-oidc';
import { ToasterModule } from 'angular2-toaster';

import { createTranslateLoader } from '../../../app.module';
import { OffsidebarComponent } from './offsidebar.component';

/* tslint:disable:no-unused-variable */

describe("Component: Offsidebar", () => {

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [
                TranslateModule.forRoot({
                    loader: {
                        provide: TranslateLoader,
                        useFactory: (createTranslateLoader),
                        deps: [HttpClient]
                    }
                }),
                HttpClientModule,
                SharedModule,
                ToasterModule.forRoot(),
                OAuthModule.forRoot()
            ],
            providers: [SettingsService, ThemesService, TranslatorService, VersionService]
        }).compileComponents();
    });

    it("should create an instance", async(inject([SettingsService, ThemesService, TranslatorService],
        (settingsService, themesService, translatorService) => {
            let component = new OffsidebarComponent(settingsService, themesService, translatorService);
            expect(component).toBeTruthy();
        })));
});
