import { OAuthService, JwksValidationHandler } from "angular-oauth2-oidc";
import { Injectable } from "@angular/core";
import { of, concat, from, Observable } from "rxjs";
import { Router } from "@angular/router";
import { environment } from "../../../environments/environment";
import { authConfig } from "../auth/auth.config";
import { withLatestFrom, map, switchMap, share } from "rxjs/operators";

@Injectable({
    providedIn: 'root' // or 'root' for singleton
})
export class SettingsService {


    private user: any;
    public app: any;
    public layout: any;
    private loadDocumentObservable: Observable<void>;

    constructor(
        private oauthService: OAuthService,
        private router: Router) {

        // App Settings
        // -----------------------------------
        this.app = {
            name: "JP Project",
            description: "User Management UI",
            year: ((new Date()).getFullYear()),
            docLoaded: false,
        };

    }

    public logout() {
        this.oauthService.logOut();
    }

    public getUserProfile(): Observable<object> {
        if (this.user == null) {
            return from(this.oauthService.loadUserProfile()).pipe(share());
        }
        return of(this.user);
    }

    public login() {
        if (!this.oauthService.hasValidIdToken() || !this.oauthService.hasValidAccessToken()) {
            this.oauthService.initImplicitFlow();
        } else {
            // for race conditions, sometimes dashboard don't load
            setTimeout(() => {
                this.router.navigate(["/home"]);
            }, 1000);
        }
    }



}