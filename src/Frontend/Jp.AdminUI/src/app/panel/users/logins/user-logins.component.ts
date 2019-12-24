import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslatorService } from '@core/translator/translator.service';
import { UserService } from '@shared/services/user.service';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { UserLogin } from '@shared/viewModel/user-login.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';
import { throwError } from 'rxjs';
import { debounceTime, flatMap, map, tap } from 'rxjs/operators';


@Component({
    selector: "app-user-logins",
    templateUrl: "./user-logins.component.html",
    styleUrls: ["./user-logins.component.scss"],
    providers: [UserService],
})
export class UserLoginsComponent implements OnInit {

    public errors: Array<string>;
    public logins: UserLogin[];

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });

    public showButtonLoading: boolean;
    public userName: string;
    public bsConfig = {
        containerClass: 'theme-angle'
    };

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private userService: UserService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.route.params.pipe(tap(p => this.userName = p["username"])).pipe(map(p => p["username"])).pipe(flatMap(m => this.userService.getUserLogins(m.toString()))).subscribe(result => this.logins = result);
        this.errors = [];
        this.showButtonLoading = false;
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }


    public remove(loginProvider: string, providerKey: string) {

        this.showButtonLoading = true;
        this.errors = [];
        try {

            this.userService.removeLogin(this.userName, loginProvider, providerKey).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.loadLogins();
                    }
                    this.showButtonLoading = false;
                },
                err => {
                    this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                    this.showButtonLoading = false;
                }
            );
        } catch (error) {
            this.errors = [];
            this.errors.push("Unknown error while trying to remove");
            this.showButtonLoading = false;
            return throwError("Unknown error while trying to remove");
        }

    }

    private loadLogins(): void {
        this.userService.getUserLogins(this.userName).subscribe(c => this.logins = c);
    }
}
