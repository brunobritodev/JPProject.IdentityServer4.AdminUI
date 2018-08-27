import { Component } from "@angular/core";
import { OAuthService } from "angular-oauth2-oidc";
import { Router } from "@angular/router";

@Component({
    selector: "app-dashboard",
    templateUrl: "login.component.html"
})
export class LoginComponent {
    constructor(private oauthService: OAuthService,
        private router: Router) {
        this.configure();
    }

    public login() {
        this.oauthService.initImplicitFlow("login");
    }

    public async configure() {
        this.oauthService.loadDiscoveryDocument().then(doc => {
            console.log("doc loaded");
            this.oauthService.tryLogin()
                .catch(err => {
                    console.error(err);
                })
                .then(() => {
                    if (!this.oauthService.hasValidIdToken() || !this.oauthService.hasValidAccessToken()) {
                        this.oauthService.initImplicitFlow();
                    } else {
                        // for race conditions, sometimes dashboard don't load
                        setTimeout(() => {
                            this.router.navigate(["/dashboard"]);
                        }, 1000);
                    }
                });
        });
    }

}
