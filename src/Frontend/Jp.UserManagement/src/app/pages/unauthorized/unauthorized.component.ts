import { Component } from "@angular/core";
import { SettingsService } from "../../core/settings/settings.service";
import { TranslatorService } from '@core/translator/translator.service';
import { OAuthenticationService } from "@core/auth/auth.service";

@Component({
  selector: "app-dashboard",
  templateUrl: "unauthorized.component.html",
  providers: [TranslatorService]
})
export class UnauthorizedComponent {
    constructor(
        public authService: OAuthenticationService,
        public translator: TranslatorService){
        
    }

    public login() {
        this.authService.login();
    }
 }