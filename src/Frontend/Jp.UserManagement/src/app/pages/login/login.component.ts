import { Component, OnInit } from "@angular/core";
import { OAuthService } from "angular-oauth2-oidc";
import { Router } from "@angular/router";
import { SettingsService } from "../../core/settings/settings.service";
import { TranslatorService } from '@core/translator/translator.service';

@Component({
    selector: "app-dashboard",
    templateUrl: "login.component.html",
    providers: [SettingsService,TranslatorService]
})
export class LoginComponent implements OnInit {
    constructor(private settingsService: SettingsService,
        private router: Router,
        public translator: TranslatorService) {
        
    }

    public ngOnInit(){
        this.login();
    }

    public login() {
        this.settingsService.login();
    }

}
