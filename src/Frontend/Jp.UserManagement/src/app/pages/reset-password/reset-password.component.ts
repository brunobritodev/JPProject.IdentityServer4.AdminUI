import { Component, OnDestroy, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { Subscription } from "rxjs";
import { UserService } from "../../shared/services/user.service";
import { ResetPassword } from "../../shared/view-model/reset-password.model";
import { DefaultResponse } from "../../shared/view-model/default-response.model";
import { TranslatorService } from '@core/translator/translator.service';

@Component({
    selector: "app-dashboard",
    templateUrl: "./reset-password.component.html",
    providers: [UserService,TranslatorService]
})
export class ResetPasswordComponent implements OnInit, OnDestroy {

    public model: ResetPassword;
    public errors: Array<string>;

    public showButtonLoading: boolean;
    public passChanged: boolean;
    private resetPassSub: Subscription;


    constructor(
        private authService: UserService,
        private router: Router,
        private route: ActivatedRoute,
        public translator: TranslatorService) {
    }


    public ngOnDestroy() {
        this.resetPassSub.unsubscribe();
    }

    public ngOnInit() {
        this.model = new ResetPassword();
        this.resetPassSub = this.route
            .queryParams
            .subscribe(params => {
                // Defaults to 0 if no query param provided.
                console.log(params);
                if (params == null || params.code == null || params.email == null) {
                    this.router.navigate(["/recover"]);
                    return;
                }
                this.model.code = params.code;
                this.model.email = params.email;
            });
        this.showButtonLoading = false;

        this.errors = [];
    }


    public async reset() {
        this.showButtonLoading = true;
        try {
            this.authService.resetPassword(this.model).subscribe(
                registerResult => {
                    this.router.navigate(["/login"]);
                    this.showButtonLoading = false;
                },
                response => {
                    this.errors = [];
                    this.errors = DefaultResponse.GetErrors(response).map(a => a.value);
                    this.showButtonLoading = false;
                }
            );

        } catch (error) {
            this.errors = [];
            this.errors.push("Unknown error while trying to reset password");
            this.showButtonLoading = false;
        }
    }
}
