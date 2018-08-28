import { Component, Input, OnInit } from "@angular/core";
import { navItems } from "../../_nav";
import { OAuthService } from "../../../../node_modules/angular-oauth2-oidc";
import { SettingsService } from "../settings/settings.service";
import { environment } from "../../../environments/environment";

@Component({
    selector: "app-dashboard",
    templateUrl: "./default-layout.component.html",
    providers: [SettingsService]
})
export class DefaultLayoutComponent implements OnInit {

    public navItems = navItems;
    public sidebarMinimized = true;
    private changes: MutationObserver;
    public element: HTMLElement = document.body;
    public userProfile: any;
    constructor(public settingsService: SettingsService) {

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
        this.settingsService.logout();
    }

    public async getUserImage() {
        this.userProfile = await this.settingsService.getUserProfile();
        if (!environment.production)
            console.table(this.userProfile);
    }
}
