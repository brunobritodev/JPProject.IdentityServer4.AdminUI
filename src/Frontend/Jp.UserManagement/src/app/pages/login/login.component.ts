import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { SettingsService } from "../../core/settings/settings.service";
import { TranslatorService } from '@core/translator/translator.service';
import { OAuthenticationService } from "@core/auth/auth.service";

@Component({
    selector: "app-dashboard",
    templateUrl: "login.component.html",
    providers: [SettingsService, TranslatorService]
})
export class LoginComponent implements OnInit {
    constructor(
        private authService: OAuthenticationService,
        private router: Router,
        public translator: TranslatorService) {
        
    }

    public ngOnInit(){
        this.authService.isAuthenticated$.subscribe(yes => {
            if (!yes)
                this.login();
        });
    }

    public login() {
        this.authService.login('/login-callback');
    }

}
