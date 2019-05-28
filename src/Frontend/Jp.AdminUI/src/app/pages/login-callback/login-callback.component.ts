import { Component, OnInit, OnDestroy } from "@angular/core";
import { Router } from "@angular/router";
import { SettingsService } from "../../core/settings/settings.service";
import { AuthService } from "@core/auth/auth.service";
import { Subscription } from "rxjs";

@Component({
    selector: "app-login-callback",
    templateUrl: "login-callback.component.html",
    styleUrls: ["./login-callback.component.scss"],
})
export class LoginCallbackComponent implements OnInit, OnDestroy {
    stream: Subscription;

    constructor(
        private authService: AuthService,
        private router: Router,
        public settingsService: SettingsService) { }


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
