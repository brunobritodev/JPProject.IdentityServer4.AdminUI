import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslatorService } from '@core/translator/translator.service';
import { UserService } from '@shared/services/user.service';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { ResetPassword } from '@shared/viewModel/reset-password.model';
import { UserProfile } from '@shared/viewModel/userProfile.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';
import * as jsonpatch from 'fast-json-patch';
import { throwError } from 'rxjs';
import { flatMap, tap } from 'rxjs/operators';


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
    public showButtonLoading: boolean = true;

    public shouldChangePass: boolean = false;
    public shouldChangeUserData: boolean = true;
    private username: string;
    patchObserver: jsonpatch.Observer<UserProfile>;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private userService: UserService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.route.params
            .pipe(tap(p => this.username = p["username"]),
                flatMap(p => this.userService.getDetails(p["username"])),
                tap(user => this.patchObserver = jsonpatch.observe(user))
            ).subscribe(result => {
                this.model = result;
                this.showButtonLoading = false;
                if (this.model.lockoutEnd != null)
                    this.model.lockoutEnd = new Date(this.model.lockoutEnd);
            }, err => {
                this.router.navigate(['/users']);
            });
        this.errors = [];
        this.resetPassword = new ResetPassword();
        
    }

    public update() {

        this.showButtonLoading = true;
        this.errors = [];
        try {

            this.userService.patch(this.username, jsonpatch.generate(this.patchObserver)).subscribe(
                () => {
                    this.showSuccessMessage();
                    this.router.navigate(["/users"]);
                },
                err => {
                    this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                    this.showButtonLoading = false;
                }
            );
        } catch (error) {
            this.errors = [];
            this.errors.push("Unknown error while trying to update");
            this.showButtonLoading = false;
            return throwError("Unknown error while trying to update");
        }
    }

    public resetPass() {

        this.showButtonLoading = true;
        this.errors = [];
        this.userService.resetPassword(this.username, this.resetPassword).subscribe(
            () => {
                this.showSuccessMessage();
                this.router.navigate(["/users"]);
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );

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
