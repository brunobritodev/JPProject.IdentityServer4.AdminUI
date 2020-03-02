import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslatorService } from '@core/translator/translator.service';
import { RoleService } from '@shared/services/role.service';
import { UserService } from '@shared/services/user.service';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { Role, SaveRole } from '@shared/viewModel/role.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';
import { Observable } from 'rxjs';
import { debounceTime, flatMap, map, tap } from 'rxjs/operators';


@Component({
    selector: "app-user-roles",
    templateUrl: "./user-roles.component.html",
    styleUrls: ["./user-roles.component.scss"],
    providers: [UserService, RoleService],
})
export class UserRolesComponent implements OnInit {

    public errors: Array<string>;
    public model: SaveRole;
    public userRoles: Role[];
    public roles: string[];

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
        public toasterService: ToasterService,
        public roleService: RoleService) { }

    public ngOnInit() {
        this.route.params.pipe(tap(p => this.userName = p["username"])).pipe(map(p => p["username"])).pipe(flatMap(m => this.userService.getUserRoles(m.toString()))).subscribe(result => this.userRoles = result);
        this.errors = [];
        this.model = new SaveRole();
        this.showButtonLoading = false;
        this.roleService.getAvailableRoles().subscribe(roles => this.roles = roles.map(r => r.name));

    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

    public remove(role: string) {

        this.showButtonLoading = true;
        this.errors = [];
        this.userService.removeRole(this.userName, role).subscribe(
            () => {
                this.showSuccessMessage();
                this.loadClaims();
                this.showButtonLoading = false;
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );
    }

    private loadClaims(): void {
        this.userService.getUserRoles(this.userName).subscribe(c => this.userRoles = c);
    }

    public save() {
        this.showButtonLoading = true;
        this.errors = [];
        this.userService.saveRole(this.userName, this.model).subscribe(
            registerResult => {
                if (registerResult) {
                    this.showSuccessMessage();
                    this.loadClaims();
                    this.model = new SaveRole();
                }
                this.showButtonLoading = false;
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );

    }

}
