import { Component } from "@angular/core";
import { SettingsService } from "../../core/settings/settings.service";

@Component({
  selector: "app-dashboard",
  templateUrl: "unauthorized.component.html"
})
export class UnauthorizedComponent {
    constructor(private settingsService: SettingsService){
        
    }

    public login() {
        this.settingsService.login();
    }
 }