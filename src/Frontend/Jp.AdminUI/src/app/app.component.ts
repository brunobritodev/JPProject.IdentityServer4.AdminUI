import { Component, HostBinding, OnInit } from "@angular/core";
declare var $: any;

import { SettingsService } from "./core/settings/settings.service";
// import { authConfig } from "./core/auth/auth.config";
import { AuthService } from "@core/auth/auth.service";
import { Observable } from "rxjs";




@Component({
    selector: "app-root",
    templateUrl: "./app.component.html",
    styleUrls: ["./app.component.scss"]
})
export class AppComponent implements OnInit {

    @HostBinding("class.layout-fixed") get isFixed() { return this.settings.layout.isFixed; }
    @HostBinding("class.aside-collapsed") get isCollapsed() { return this.settings.layout.isCollapsed; }
    @HostBinding("class.layout-boxed") get isBoxed() { return this.settings.layout.isBoxed; }
    @HostBinding("class.layout-fs") get useFullLayout() { return this.settings.layout.useFullLayout; }
    @HostBinding("class.hidden-footer") get hiddenFooter() { return this.settings.layout.hiddenFooter; }
    @HostBinding("class.layout-h") get horizontal() { return this.settings.layout.horizontal; }
    @HostBinding("class.aside-float") get isFloat() { return this.settings.layout.isFloat; }
    @HostBinding("class.offsidebar-open") get offsidebarOpen() { return this.settings.layout.offsidebarOpen; }
    @HostBinding("class.aside-toggled") get asideToggled() { return this.settings.layout.asideToggled; }
    @HostBinding("class.aside-collapsed-text") get isCollapsedText() { return this.settings.layout.isCollapsedText; }

    isAuthenticated: Observable<boolean>;
    isDoneLoading: Observable<boolean>;
    canActivateProtectedRoutes: Observable<boolean>;

    constructor(
        private authService: AuthService,
        public settings: SettingsService
    ) {
        this.isAuthenticated = this.authService.isAuthenticated$;
        this.isDoneLoading = this.authService.isDoneLoading$;
        this.canActivateProtectedRoutes = this.authService.canActivateProtectedRoutes$;

        this.authService.runInitialLoginSequence(); 
    }


    ngOnInit() {
        $(document).on("click", "[href=\"#\"]", e => e.preventDefault());
    }
}
