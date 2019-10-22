import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslatorService } from '@core/translator/translator.service';
import { UserService } from '@shared/services/user.service';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { UserProfile } from '@shared/viewModel/userProfile.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';
import { Observable, Subject } from 'rxjs';
import { debounceTime, switchMap } from 'rxjs/operators';


@Component({
    selector: "app-user-add",
    templateUrl: "./user-add.component.html",
    styleUrls: ["./user-add.component.scss"],
    providers: [UserService],
    encapsulation: ViewEncapsulation.None
})
export class UserAddComponent implements OnInit {

    public errors: Array<string>;
    public model: UserProfile;
    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public bsConfig = {
        containerClass: 'theme-angle'
    };
    public showButtonLoading: boolean;
    private userExistsSubject: Subject<string> = new Subject<string>();
    private emailExistsSubject: Subject<string> = new Subject<string>();
    userExist: boolean;
    emailExist: boolean;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private userService: UserService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.model = new UserProfile();
        this.errors = [];
        this.showButtonLoading = false;
        this.userExistsSubject
            .pipe(debounceTime(500))
            .pipe(switchMap(a => this.userService.checkUserName(a)))
            .subscribe((response: boolean) => {
                this.userExist = response;
            });

        this.emailExistsSubject
            .pipe(debounceTime(500))
            .pipe(switchMap(a => this.userService.checkEmail(a)))
            .subscribe((response: boolean) => {
                this.emailExist = response;
            });
    }

    public save() {

        this.showButtonLoading = true;
        this.errors = [];
        this.userService.save(this.model).subscribe(
            registerResult => {
                if (registerResult) {
                    this.showSuccessMessage();
                    this.router.navigate(["/users", this.model.userName, 'edit']);
                }
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

    public checkIfEmailExists() {
        if (this.model.email == null || this.model.email === "")
            return;

        if (!this.model.isValidEmail())
            return;

        this.emailExistsSubject.next(this.model.email);
    }

    public checkIfUniquenameExists() {
        if (this.model.userName == null || this.model.userName === "")
            return;
        this.userExistsSubject.next(this.model.userName);
    }

    public getClassUsernameExist(): string {
        if (this.model.userName == null || this.model.userName === "")
            return "";

        return this.userExist ? "is-invalid" : "is-valid";
    }

    public getClassEmailExist(): string {
        if (this.model.email == null || this.model.email === "")
            return "";

        return !this.model.isValidEmail() || this.emailExist ? "is-invalid" : "is-valid";
    }
}
