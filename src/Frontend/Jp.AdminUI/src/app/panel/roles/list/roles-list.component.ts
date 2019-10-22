import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslatorService } from '@core/translator/translator.service';
import { RoleService } from '@shared/services/role.service';
import { UserService } from '@shared/services/user.service';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { Role } from '@shared/viewModel/role.model';
import { UserProfile } from '@shared/viewModel/userProfile.model';
import { ToasterService } from 'angular2-toaster';
import { Observable } from 'rxjs';
import Swal from 'sweetalert2';

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
        public toasterService: ToasterService) { }

    ngOnInit() {
        this.errors = [];
        this.loadRoles();
    }

    public loadRoles() {
        this.roleService.getAvailableRoles().subscribe(a => {
            this.roles = a;
        });
    }

    public remove(name: string) {
        this.translator.translate.get('persistedGrant.remove').subscribe(m => {
            Swal.fire({
                title: m['title'],
                text: m["text"],
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: m["confirmButtonText"],
                cancelButtonText: m["cancelButtonText"],

            }).then(isConfirm => {
                if (isConfirm) {
                    this.selectedRole = null;
                    this.roleService.remove(name).subscribe(
                        registerResult => {
                            this.loadRoles();
                            Swal.fire("Deleted!", m["deleted"], 'success');
                        },
                        err => {
                            let errors = ProblemDetails.GetErrors(err).map(a => a.value);
                            Swal.fire("Error!", errors[0], 'error');
                        }
                    );


                } else {
                    Swal.fire("Cancelled", m["cancelled"], 'error');
                }
            });
        });
    }

    public removeFromRole(user: string, role: string) {
        this.showButtonLoading = true;
        this.errors = [];

        this.roleService.removeUserFromRole(user, role).subscribe(
            registerResult => {
                this.details(this.selectedRole);
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

    public details(role: string) {
        this.selectedRole = role;
        this.users$ = this.roleService.getUsersFromRole(role);
    }
}
