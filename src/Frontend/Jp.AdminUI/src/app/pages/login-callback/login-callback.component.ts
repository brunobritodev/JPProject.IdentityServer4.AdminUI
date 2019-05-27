import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { SettingsService } from "../../core/settings/settings.service";
import { AuthService } from "@core/auth/auth.service";
import { tap } from "rxjs/operators";

@Component({
    selector: "app-login-callback",
    templateUrl: "login-callback.component.html",
    styleUrls: ["./login-callback.component.scss"],
})
export class LoginCallbackComponent implements OnInit {

    constructor(
        private authService: AuthService,
        private router: Router,
        public settingsService: SettingsService) { }

    ngOnInit() {
        this.authService.canActivateProtectedRoutes$
            .subscribe(yes => {
                if (yes)
                    return this.router.navigate(['/home']);
            });
    }
}
