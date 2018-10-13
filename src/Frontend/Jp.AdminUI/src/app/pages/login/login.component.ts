import { Component, OnInit } from "@angular/core";
import { SettingsService } from "../../core/settings/settings.service";
import { Router } from "@angular/router";

@Component({
    selector: "app-login",
    templateUrl: "./login.component.html",
    styleUrls: ["./login.component.scss"],
    providers: []
})
export class LoginComponent implements OnInit {
    constructor(private settingsService: SettingsService,
        private router: Router) {

    }

    public ngOnInit() {
        this.login();
    }

    public login() {
        this.settingsService.login();
    }

}
