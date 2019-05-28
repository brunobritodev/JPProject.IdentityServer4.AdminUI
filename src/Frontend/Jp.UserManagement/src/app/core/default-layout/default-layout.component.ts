import { Component, Input, OnInit } from "@angular/core";
import { navItems } from "../../_nav";
import { SettingsService } from "../settings/settings.service";
import { tap } from "rxjs/operators";
import { environment } from "@env/environment";
import { Router } from "@angular/router";
import { TranslatorService } from "../translator/translator.service";
import { OAuthenticationService } from "../auth/auth.service";
import { Observable } from "rxjs";

@Component({
    selector: "app-dashboard",
    templateUrl: "./default-layout.component.html",
    providers: [SettingsService, TranslatorService]
})
export class DefaultLayoutComponent implements OnInit {

    public navItems = navItems;
    public sidebarMinimized = true;
    private changes: MutationObserver;
    public element: HTMLElement = document.body;
    public userProfile$: Observable<object>;
    constructor(public settingsService: SettingsService,
        public authService: OAuthenticationService,
        private router: Router,
        public translator: TranslatorService) {
        this.changes = new MutationObserver((mutations) => {
            this.sidebarMinimized = document.body.classList.contains("sidebar-minimized");
        });

        this.changes.observe(<Element>this.element, {
            attributes: true
        });
    }

    public ngOnInit() {
        this.getUserImage();
    }

    public logout() {
        this.authService.logout();
    }

    public setLang(value) {
        this.translator.useLanguage(value);
    }

    public async getUserImage() {
        this.userProfile$ = this.settingsService.getUserProfile()
            .pipe(
                tap(u => {
                    if (!environment.production)
                        console.table(u);
                }));
    }
}
