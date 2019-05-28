import { Injectable } from "@angular/core";
import { UserProfile } from "@shared/viewModel/userProfile.model";
import { HttpClient } from "@angular/common/http";
import { OAuthService } from "angular-oauth2-oidc";
import { of, from, Observable, defer } from "rxjs";
import { Router } from "@angular/router";
import { map, switchMap, share, tap } from "rxjs/operators";

declare var $: any;

@Injectable()
export class SettingsService {

    private user: UserProfile;
    public app: any;
    public layout: any;
    userProfileObservable: Observable<object>;
    loadDiscoveryDocumentAndTryLoginObservable: Observable<any>;
    doc: any;

    constructor(
        private http: HttpClient,
        private oauthService: OAuthService,
        private router: Router) {


        // App Settings
        // -----------------------------------
        this.app = {
            name: "Jp Project - IS4Admin",
            description: "IdentityServer4 Admin Panel",
            year: ((new Date()).getFullYear()),
            version: "1.0.0"
        };

        // Layout Settings
        // -----------------------------------
        let savedLayout = localStorage.getItem("LayoutSettings");
        if (savedLayout == null)
            this.layout = {
                isFixed: true,
                isCollapsed: false,
                isBoxed: true,
                isRTL: false,
                horizontal: false,
                isFloat: false,
                asideHover: false,
                theme: null,
                asideScrollbar: false,
                isCollapsedText: false,
                useFullLayout: false,
                hiddenFooter: false,
                offsidebarOpen: false,
                asideToggled: false,
                viewAnimation: "ng-fadeInUp"
            };
        else {
            this.layout = JSON.parse(savedLayout);
            this.layout.offsidebarOpen = false;
        }

        /**
         * Defer makes promise cold
         * https://blog.angularindepth.com/observable-frompromise-cold-or-hot-531229818255
         */
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

    public saveLayout() {
        localStorage.setItem("LayoutSettings", JSON.stringify(this.layout));
    }

    public getAppSetting(name) {
        return name ? this.app[name] : this.app;
    }
    public getUserSetting(name) {
        return name ? this.user[name] : this.user;
    }
    public getLayoutSetting(name) {
        return name ? this.layout[name] : this.layout;
    }

    public setAppSetting(name, value) {
        if (typeof this.app[name] !== "undefined")
            this.app[name] = value;
    }
    public setUserSetting(name, value) {
        if (typeof this.user[name] !== "undefined")
            this.user[name] = value;
    }
    public setLayoutSetting(name, value) {
        if (typeof this.layout[name] !== "undefined")
            return this.layout[name] = value;
    }

    public toggleLayoutSetting(name) {
        return this.setLayoutSetting(name, !this.getLayoutSetting(name));
    }

}
