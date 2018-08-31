import { Component, Input, OnInit } from "@angular/core";
import { navItems } from "../../_nav";
import { SettingsService } from "../settings/settings.service";
import { tap } from "rxjs/operators";
import { environment } from "../../../environments/environment";
import { Router } from "@angular/router";

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
    constructor(public settingsService: SettingsService,
        private router: Router) {
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
        this.settingsService.getUserProfile()
             .pipe(
                 tap(u => {
                     if (!environment.production)
                         console.table(u);
                 }))
            .subscribe(a => this.userProfile = a);
    }
}
