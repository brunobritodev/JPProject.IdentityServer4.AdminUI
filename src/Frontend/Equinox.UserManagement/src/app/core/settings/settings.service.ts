import { OAuthService } from "angular-oauth2-oidc";
import { Injectable } from "@angular/core";
import { of } from "rxjs";
import { Router } from "@angular/router";

@Injectable()
export class SettingsService {

    private user: any;
    public app: any;
    public layout: any;

    constructor(
        private oauthService: OAuthService,
        private router: Router) {

        // App Settings
        // -----------------------------------
        this.app = {
            name: "JP Project",
            description: "User Management UI",
            year: ((new Date()).getFullYear())
        };
    }

    public logout() {
        this.oauthService.logOut();
    }

    public getUserProfile(): Promise<object> {
        if (this.user == null) {
            return this.oauthService.loadUserProfile()
                .then(userProfile =>
                    this.user = userProfile
                );
        }
        return of(this.user).toPromise();
    }

    public login() {
        if (!this.oauthService.hasValidIdToken() || !this.oauthService.hasValidAccessToken()) {
            this.oauthService.initImplicitFlow();
        } else {
            // for race conditions, sometimes dashboard don't load
            setTimeout(() => {
                this.router.navigate(["/dashboard"]);
            }, 1000);
        }
    }





}