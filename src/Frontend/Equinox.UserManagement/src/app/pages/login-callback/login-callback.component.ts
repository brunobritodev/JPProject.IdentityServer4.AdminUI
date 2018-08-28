import { Component, OnInit } from "@angular/core";
import { OAuthService, AuthConfig } from "angular-oauth2-oidc";
import { Router } from "@angular/router";

@Component({
    selector: "app-dashboard",
    templateUrl: "login-callback.component.html"
})
export class LoginCallbackComponent implements OnInit {

    constructor(private oauthService: OAuthService, private router: Router) { }

    ngOnInit() {
        this.oauthService.loadDiscoveryDocumentAndTryLogin().then(doc => {
            if (!this.oauthService.hasValidIdToken() || !this.oauthService.hasValidAccessToken()) {
                this.oauthService.initImplicitFlow();
            } else {
                // for race conditions, sometimes dashboard don't load
                setTimeout(() => {
                    this.router.navigate(["/home"]);
                }, 1000);
            }
        });

    }
}