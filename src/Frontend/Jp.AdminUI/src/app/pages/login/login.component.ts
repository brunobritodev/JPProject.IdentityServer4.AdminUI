import { Component, OnInit } from "@angular/core";
import { SettingsService } from "../../core/settings/settings.service";
import { Router } from "@angular/router";
import { AuthService } from "@core/auth/auth.service";



@Component({
    selector: "app-login",
    templateUrl: "./login.component.html",
    styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit {
    constructor(public settingsService: SettingsService,
        private authService: AuthService,
        private router: Router) {

    }

    public ngOnInit() {
        this.authService.isAuthenticated$.subscribe(yes => {
            if (!yes)
                this.login();
        });
    }

    public login() {
        this.authService.login('/login-callback');
    }

}
