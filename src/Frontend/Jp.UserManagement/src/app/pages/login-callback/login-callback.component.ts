import { Component, OnInit } from "@angular/core";
import { OAuthService, AuthConfig } from "angular-oauth2-oidc";
import { Router } from "@angular/router";
import { environment } from "@env/environment";
import { SettingsService } from "../../core/settings/settings.service";
import { TranslatorService } from '@core/translator/translator.service';
import { OAuthenticationService } from "@core/auth/auth.service";

@Component({
    selector: "app-dashboard",
    templateUrl: "login-callback.component.html",
    providers: [SettingsService, TranslatorService]
})
export class LoginCallbackComponent implements OnInit {

    constructor(
        private authService: OAuthenticationService,
        private router: Router,
        private settingsService: SettingsService,
        public translator: TranslatorService) { }

    ngOnInit() {
        this.authService.canActivateProtectedRoutes$.subscribe(yes => {
            if (yes)
                return this.router.navigate(['/home']);
        });
    }
}