import { Component } from "@angular/core";
import { SettingsService } from "../../core/settings/settings.service";
import { TranslatorService } from '@core/translator/translator.service';

@Component({
  selector: "app-dashboard",
  templateUrl: "unauthorized.component.html",
  providers: [TranslatorService]
})
export class UnauthorizedComponent {
    constructor(private settingsService: SettingsService,public translator: TranslatorService){
        
    }

    public login() {
        this.settingsService.login();
    }
 }