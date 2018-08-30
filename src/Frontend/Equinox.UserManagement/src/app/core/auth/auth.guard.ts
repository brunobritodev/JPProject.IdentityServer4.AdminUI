import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { OAuthService } from "angular-oauth2-oidc";
import { of } from "rxjs";
import { SettingsService } from "../settings/settings.service";
import { map } from "rxjs/operators";

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(
        private router: Router,
        private oauthService: OAuthService,
        private settingsService: SettingsService) { }

    canActivate() {

        return this.settingsService.loadDiscoveryDocumentAndTryLogin().pipe(map(a => this.oauthService.hasValidIdToken())).pipe(tokenValid => {
            if (!tokenValid)
                this.router.navigate(["/unauthorized"]);

            return tokenValid;
        });
        
    }
}