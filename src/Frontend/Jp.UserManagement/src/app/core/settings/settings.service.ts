import { OAuthService } from "angular-oauth2-oidc";
import { Injectable } from "@angular/core";
import { of, from, Observable, defer } from "rxjs";
import { Router } from "@angular/router";
import { map, switchMap, share, tap } from "rxjs/operators";

@Injectable({providedIn: 'root'})
export class SettingsService {


    private user: any;
    public app: any;
    public layout: any;
    doc: any;
    userProfileObservable: Observable<object>;
    loadDiscoveryDocumentAndTryLoginObservable: Observable<any>;

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

        this.userProfileObservable = defer(() => from(this.oauthService.loadUserProfile())).pipe(share());
        this.loadDiscoveryDocumentAndTryLoginObservable = defer(() => from(this.oauthService.loadDiscoveryDocument())).pipe(share()).pipe(tap(a => this.doc = a)).pipe(switchMap(a => this.oauthService.tryLogin())).pipe(map(() => this.doc));
    }

    
    public getUserProfile(): Observable<object> {
        if (this.user == null) {
            return this.userProfileObservable;
        }
        return of(this.user);
    }

    set userpicture(image: string) {
        this.user.picture = image;
    }

}