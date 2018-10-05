import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { TranslatorService } from "../../../core/translator/translator.service";
import { flatMap, tap, map, debounceTime } from "rxjs/operators";
import { ActivatedRoute, Router } from "@angular/router";
import { ToasterConfig, ToasterService } from "angular2-toaster";
import { DefaultResponse } from "../../../shared/viewModel/default-response.model";
import { Observable } from "rxjs";
import { UserService } from "../user.service";
import { UserRole } from "../../../shared/viewModel/user-role.model";


@Component({
    selector: "app-user-roles",
    templateUrl: "./user-roles.component.html",
    styleUrls: ["./user-roles.component.scss"],
    providers: [UserService],
})
export class UserRolesComponent implements OnInit {

    public errors: Array<string>;
    public model: UserRole;
    public userRoles: UserRole[];

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public showButtonLoading: boolean;
    public userName: string;
    public bsConfig = {
        containerClass: 'theme-angle'
    };
    public standardClaims: string[];

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private userService: UserService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.route.params.pipe(tap(p => this.userName = p["username"])).pipe(map(p => p["username"])).pipe(flatMap(m => this.userService.getUserRoles(m.toString()))).subscribe(result => this.userRoles = result.data);
        this.errors = [];
        this.model = new UserRole();
        this.showButtonLoading = false;
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

    public remove(role: string) {

        this.showButtonLoading = true;
        try {

            this.userService.removeClaim(this.userName, role).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.loadClaims();
                    }
                    this.showButtonLoading = false;
                },
                err => {
                    this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                    if (this.errors[0] == undefined) {
                        this.errors = [];
                        this.errors.push("Unknown error while trying to remove");
                    }
                    this.showButtonLoading = false;
                }
            );
        } catch (error) {
            this.errors = [];
            this.errors.push("Unknown error while trying to remove");
            this.showButtonLoading = false;
            return Observable.throw("Unknown error while trying to remove");
        }

    }

    private loadClaims(): void {
        this.userService.getUserRoles(this.userName).subscribe(c => this.userRoles = c.data);
    }

    public save() {
        this.showButtonLoading = true;
        try {
            this.model.userName = this.userName;
            this.userService.saveRole(this.model).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.loadClaims();
                        this.model = new UserRole();
                    }
                    this.showButtonLoading = false;
                },
                err => {
                    this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                    if (this.errors[0] == undefined) {
                        this.errors = [];
                        this.errors.push("Unknown error while trying to register");
                    }
                    this.showButtonLoading = false;
                }
            );
        } catch (error) {
            this.errors = [];
            this.errors.push("Unknown error while trying to register");
            this.showButtonLoading = false;
            return Observable.throw("Unknown error while trying to register");
        }
    }

}
