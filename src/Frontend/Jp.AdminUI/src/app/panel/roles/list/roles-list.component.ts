import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";
import { RoleService } from "@shared/services/role.service";
import { Role } from "@shared/viewModel/role.model";
import { Observable } from "rxjs";
import { UserService } from "@shared/services/user.service";
import { UserProfile } from "@shared/viewModel/userProfile.model";
import { ToasterService } from "angular2-toaster";
import { Router } from "@angular/router";
import { DefaultResponse } from "@shared/viewModel/default-response.model";

const swal = require('sweetalert');

@Component({
    selector: "app-roles-list",
    templateUrl: "./roles-list.component.html",
    styleUrls: ["./roles-list.component.scss"],
    providers: [RoleService, UserService]
})
export class RolesListComponent implements OnInit {

    public roles: Role[];
    public users$: Observable<UserProfile[]>;
    public selectedRole: string;
    showButtonLoading: boolean;
    public errors: string[];

    constructor(
        private router: Router,
        public translator: TranslatorService,
        private roleService: RoleService,
        private userService: UserService,
        public toasterService: ToasterService) { }

    ngOnInit() {
        this.errors = [];
        this.loadRoles();
    }

    public loadRoles() {
        this.roleService.getAvailableRoles().subscribe(a => {
            this.roles = a.data;
        });
    }

    public remove(name: string) {
        this.translator.translate.get('persistedGrant.remove').subscribe(m => {
            swal({
                title: m['title'],
                text: m["text"],
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: m["confirmButtonText"],
                cancelButtonText: m["cancelButtonText"],
                closeOnConfirm: false,
                closeOnCancel: false
            }, (isConfirm) => {
                if (isConfirm) {
                    this.selectedRole = null;
                    this.roleService.remove(name).subscribe(
                        registerResult => {
                            if (registerResult.data) {
                                this.loadRoles();
                                swal("Deleted!", m["deleted"], 'success');
                            }
                        },
                        err => {
                            let errors = DefaultResponse.GetErrors(err).map(a => a.value);
                            swal("Error!", errors[0], 'error');
                        }
                    );


                } else {
                    swal("Cancelled", m["cancelled"], 'error');
                }
            });
        });
    }

    public removeFromRole(user: string, role: string) {
        this.showButtonLoading = true;
        this.errors = [];
        try {

            this.roleService.removeUserFromRole(user, role).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.details(this.selectedRole);
                    }
                },
                err => {
                    this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
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

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

    public details(role: string) {
        this.selectedRole = role;
        this.users$ = this.userService.getUsersFromRole(role);
    }
}
