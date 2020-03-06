import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '@core/auth/auth.service';
import { Subscription } from 'rxjs';

import { SettingsService } from '../../core/settings/settings.service';


@Component({
    selector: "app-login",
    templateUrl: "./login.component.html",
    styleUrls: ["./login.component.scss"],
})
export class LoginComponent implements OnInit, OnDestroy {
    private stream: Subscription;

    constructor(public settingsService: SettingsService,
        private authService: AuthService,
        private router: Router) {

    }

    public ngOnInit() {
        this.stream = this.authService.canActivateProtectedRoutes$.subscribe(yes => {
            if (yes)
                this.router.navigate(['/home']);
        });
    }

    public ngOnDestroy() {
        this.stream.unsubscribe();
    }

    public login() {
        this.authService.login('/login-callback');
    }
}
