import { Component, OnInit, OnDestroy } from "@angular/core";
import { Router } from "@angular/router";
import { SettingsService } from "../../core/settings/settings.service";
import { TranslatorService } from '@core/translator/translator.service';
import { OAuthenticationService } from "@core/auth/auth.service";
import { Subscription } from "rxjs";

@Component({
    selector: "app-dashboard",
    templateUrl: "login-callback.component.html",
    providers: [SettingsService, TranslatorService]
})
export class LoginCallbackComponent implements OnInit, OnDestroy {
    stream: Subscription;

    constructor(
        private authService: OAuthenticationService,
        private router: Router,
        private settingsService: SettingsService,
        public translator: TranslatorService) { }

    public ngOnInit() {
        this.stream = this.authService.canActivateProtectedRoutes$.subscribe(yes => {
            if (yes)
                return this.router.navigate(['/home']);
            else
                return this.router.navigate(['/login']);
        });
    }

    public ngOnDestroy() {
        this.stream.unsubscribe();
    }
}