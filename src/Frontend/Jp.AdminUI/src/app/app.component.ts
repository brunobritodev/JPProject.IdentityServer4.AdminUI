import { Component, HostBinding, OnInit } from "@angular/core";
declare var $: any;

import { SettingsService } from "./core/settings/settings.service";
import { OAuthService, JwksValidationHandler } from "angular-oauth2-oidc";
import { Router } from "@angular/router";
import { authConfig } from "./core/auth/auth.config";
import { environment } from "../environments/environment";
import { tap } from "rxjs/operators";

@Component({
    selector: "app-root",
    templateUrl: "./app.component.html",
    styleUrls: ["./app.component.scss"]
})
export class AppComponent implements OnInit {

    @HostBinding("class.layout-fixed") get isFixed() { return this.settings.layout.isFixed; }
    @HostBinding("class.aside-collapsed") get isCollapsed() { return this.settings.layout.isCollapsed; }
    @HostBinding("class.layout-boxed") get isBoxed() { return this.settings.layout.isBoxed; }
    @HostBinding("class.layout-fs") get useFullLayout() { return this.settings.layout.useFullLayout; }
    @HostBinding("class.hidden-footer") get hiddenFooter() { return this.settings.layout.hiddenFooter; }
    @HostBinding("class.layout-h") get horizontal() { return this.settings.layout.horizontal; }
    @HostBinding("class.aside-float") get isFloat() { return this.settings.layout.isFloat; }
    @HostBinding("class.offsidebar-open") get offsidebarOpen() { return this.settings.layout.offsidebarOpen; }
    @HostBinding("class.aside-toggled") get asideToggled() { return this.settings.layout.asideToggled; }
    @HostBinding("class.aside-collapsed-text") get isCollapsedText() { return this.settings.layout.isCollapsedText; }

    constructor(private router: Router,
        private oauthService: OAuthService,
        public settings: SettingsService) {
            this.configureWithNewConfigApi();
    }

    private async configureWithNewConfigApi() {
        this.oauthService.configure(authConfig);
        this.oauthService.setStorage(localStorage);
        this.oauthService.tokenValidationHandler = new JwksValidationHandler();

        this.settings.loadDiscoveryDocumentAndTryLogin().pipe(tap(doc => {
            if (!environment.production)
                console.log(doc);
        })).subscribe();
        // this.oauthService.loadDiscoveryDocument().then(doc => {
        //     if (!environment.production)
        //     console.log(doc);
        //     this.oauthService.tryLogin();
        // });
    }

    ngOnInit() {
        $(document).on("click", "[href=\"#\"]", e => e.preventDefault());
    }
}
