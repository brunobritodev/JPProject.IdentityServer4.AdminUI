import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";
import { flatMap, tap } from "rxjs/operators";
import { ActivatedRoute, Router } from "@angular/router";
import { ToasterConfig, ToasterService } from "angular2-toaster";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { Observable } from "rxjs";
import { UserProfile } from "@shared/viewModel/userProfile.model";
import { UserService } from "@shared/services/user.service";
import { ResetPassword } from "@shared/viewModel/reset-password.model";


@Component({
    selector: "app-user-edit",
    templateUrl: "./user-edit.component.html",
    styleUrls: ["./user-edit.component.scss"],
    providers: [UserService],
    encapsulation: ViewEncapsulation.None
})
export class UserEditComponent implements OnInit {

    public errors: Array<string>;
    public model: UserProfile;
    public resetPassword: ResetPassword;

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public bsConfig = {
        containerClass: 'theme-angle'
    };
    public showButtonLoading: boolean;

    public shouldChangePass: boolean = false;
    public shouldChangeUserData: boolean = true;
    private username: string;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private userService: UserService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.route.params.pipe(tap(p => this.username = p["username"])).pipe(flatMap(p => this.userService.getDetails(p["username"]))).subscribe(result => {
            this.model = result.data;
            if (this.model.lockoutEnd != null)
                this.model.lockoutEnd = new Date(this.model.lockoutEnd);
        });
        this.errors = [];
        this.resetPassword = new ResetPassword();
        this.showButtonLoading = false;
    }

    public update() {

        this.showButtonLoading = true;
        this.errors = [];
        try {

            this.userService.update(this.model).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.router.navigate(["/users"]);
                    }
                },
                err => {
                    this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                    this.showButtonLoading = false;
                }
            );
        } catch (error) {
            this.errors = [];
            this.errors.push("Unknown error while trying to update");
            this.showButtonLoading = false;
            return Observable.throw("Unknown error while trying to update");
        }

    }

    public resetPass() {

        this.showButtonLoading = true;
        this.errors = [];
        try {
            this.resetPassword.username = this.username;
            this.userService.resetPassword(this.resetPassword).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.router.navigate(["/users"]);
                    }
                },
                err => {
                    this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                    this.showButtonLoading = false;
                }
            );
        } catch (error) {
            this.errors = [];
            this.errors.push("Unknown error while trying to update");
            this.showButtonLoading = false;
            return Observable.throw("Unknown error while trying to update");
        }

    }

    public showChangePass() {
        this.shouldChangePass = true;
        this.shouldChangeUserData = false;
    }

    public showChangeData() {
        this.shouldChangePass = false;
        this.shouldChangeUserData = true;
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }
}
