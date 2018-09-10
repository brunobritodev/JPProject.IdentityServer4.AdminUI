import { Component, OnInit } from "@angular/core";
import { SettingsService } from "../../core/settings/settings.service";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { OAuthService, JwksValidationHandler } from "angular-oauth2-oidc";
import { authConfig } from "../../core/auth/auth.config";
import { environment } from "../../../environments/environment";
import { tap } from "rxjs/operators";

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
