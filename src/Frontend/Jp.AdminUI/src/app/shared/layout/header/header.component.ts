import { Component, OnInit, ViewChild } from "@angular/core";
const screenfull = require("screenfull");
const browser = require("jquery.browser");
declare var $: any;

import { UserblockService } from "../sidebar/userblock/userblock.service";
import { SettingsService } from "@core/settings/settings.service";
import { MenuService } from "@core/menu/menu.service";
import { Router } from "@angular/router";
import { environment } from "@env/environment";
import { AuthService } from "@core/auth/auth.service";

@Component({
    selector: "app-header",
    templateUrl: "./header.component.html",
    styleUrls: ["./header.component.scss"],
    providers: []
})
export class HeaderComponent implements OnInit {

    navCollapsed = true; // for horizontal layout
    menuItems = []; // for horizontal layout

    isNavSearchVisible: boolean;
    @ViewChild("fsbutton") fsbutton;  // the fullscreen button
    public ssoUri: string;

    constructor(
        public menu: MenuService,
        public userblockService: UserblockService,
        public settings: SettingsService,
        public authService: AuthService,
        private router: Router) {
        // show only a few items on demo
        this.menuItems = menu.getMenu().slice(0, 4); // for horizontal layout

    }

    ngOnInit() {
        this.ssoUri = environment.IssuerUri;
        this.isNavSearchVisible = false;
        if (browser.msie) { // Not supported under IE
            this.fsbutton.nativeElement.style.display = "none";
        }
    }

    public async logout() {
        await this.authService.logout();
    }

    toggleUserBlock(event) {
        event.preventDefault();
        this.userblockService.toggleVisibility();
    }

    openNavSearch(event) {
        event.preventDefault();
        event.stopPropagation();
        this.setNavSearchVisible(true);
    }

    setNavSearchVisible(stat: boolean) {
        // console.log(stat);
        this.isNavSearchVisible = stat;
    }

    getNavSearchVisible() {
        return this.isNavSearchVisible;
    }

    toggleOffsidebar() {
        this.settings.layout.offsidebarOpen = !this.settings.layout.offsidebarOpen;
    }

    toggleCollapsedSideabar() {
        this.settings.layout.isCollapsed = !this.settings.layout.isCollapsed;
    }

    isCollapsedText() {
        return this.settings.layout.isCollapsedText;
    }

    toggleFullScreen(event) {

        if (screenfull.enabled) {
            screenfull.toggle();
        }
        // Switch icon indicator
        let el = $(this.fsbutton.nativeElement);
        if (screenfull.isFullscreen) {
            el.children("em").removeClass("fa-expand").addClass("fa-compress");
        }
        else {
            el.children("em").removeClass("fa-compress").addClass("fa-expand");
        }
    }
}
