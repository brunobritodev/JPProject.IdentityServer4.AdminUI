/* tslint:disable:no-unused-variable */
import { TestBed, async, inject } from "@angular/core/testing";
import { SidebarComponent } from "./sidebar.component";
import { RouterModule, Router } from "@angular/router";

import { MenuService } from "@core/menu/menu.service";
import { SettingsService } from "@core/settings/settings.service";
import { HttpClientModule } from "@angular/common/http";
import { OAuthModule } from 'angular-oauth2-oidc';
import { VersionService } from '@shared/services/version.service';

describe("Component: Sidebar", () => {
    let mockRouter = {
        navigate: jasmine.createSpy("navigate")
    };
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpClientModule, OAuthModule.forRoot()],
            providers: [
                MenuService,
                SettingsService,
                { provide: Router, useValue: mockRouter },
                VersionService
            ]
        }).compileComponents();
    });

    it("should create an instance", async(inject([MenuService, SettingsService, Router], (menuService, settingsService, router) => {
        let component = new SidebarComponent(menuService, settingsService, router);
        expect(component).toBeTruthy();
    })));
});
