import { Component, OnInit } from "@angular/core";
import { Router, NavigationEnd } from "@angular/router";
import { OAuthService, JwksValidationHandler } from "angular-oauth2-oidc";
import { authConfig } from "./core/auth/auth.config";
import { environment } from "../environments/environment";
import { SettingsService } from "./core/settings/settings.service";
import { tap } from "rxjs/operators";
import { TranslatorService } from '@core/translator/translator.service';

@Component({
    // tslint:disable-next-line
    selector: 'body',
    template: "<router-outlet></router-outlet>",
    providers: [TranslatorService]
})
export class AppComponent implements OnInit {
    constructor(private router: Router,
        private oauthService: OAuthService,
        private settingsService: SettingsService,
        public translator: TranslatorService) {
        this.configureWithNewConfigApi();
    }

    private async configureWithNewConfigApi() {
        this.oauthService.configure(authConfig);
        this.oauthService.setStorage(localStorage);
        this.oauthService.tokenValidationHandler = new JwksValidationHandler();

        this.settingsService.loadDiscoveryDocumentAndTryLogin().pipe(tap(doc => {
            if (!environment.production)
                console.log(doc);
        })).subscribe(a => {
            this.oauthService.setupAutomaticSilentRefresh();
        });
        // this.oauthService.loadDiscoveryDocument().then(doc => {
        //     if (!environment.production)
        //     console.log(doc);
        //     this.oauthService.tryLogin();
        // });
    }

    ngOnInit() {
        this.router.events.subscribe((evt) => {
            if (!(evt instanceof NavigationEnd)) {
                return;
            }
            window.scrollTo(0, 0);
        });
    }
}