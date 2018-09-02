import { Component, OnInit } from "@angular/core";
import { OAuthService, AuthConfig } from "angular-oauth2-oidc";
import { Router } from "@angular/router";
import { environment } from "../../../environments/environment";
import { SettingsService } from "../../core/settings/settings.service";

@Component({
    selector: "app-dashboard",
    templateUrl: "login-callback.component.html"
})
export class LoginCallbackComponent implements OnInit {

    constructor(
        private oauthService: OAuthService,
        private router: Router,
        private settingsService: SettingsService) { }

    ngOnInit() {
        this.settingsService.loadDiscoveryDocumentAndTryLogin().subscribe(doc => {
            if (!environment.production)
                console.log(doc);

            if (!this.oauthService.hasValidIdToken()) {
                this.oauthService.initImplicitFlow();
            } else {
                // for race conditions, sometimes home don't load
                setTimeout(() => {
                    this.router.navigate(["/home"]);
                }, 1000);
            }
        });
    }
}