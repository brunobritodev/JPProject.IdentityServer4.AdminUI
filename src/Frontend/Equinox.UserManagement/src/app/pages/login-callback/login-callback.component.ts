import { Component, OnInit } from "@angular/core";
import { OAuthService, AuthConfig } from "angular-oauth2-oidc";
import { Router } from "@angular/router";
import { environment } from "../../../environments/environment";

@Component({
    selector: "app-dashboard",
    templateUrl: "login-callback.component.html"
})
export class LoginCallbackComponent implements OnInit {

    constructor(private oauthService: OAuthService, private router: Router) { }

    ngOnInit() {
        this.oauthService.loadDiscoveryDocument().then(doc => {
            if (!environment.production)
                console.log(doc);
                
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