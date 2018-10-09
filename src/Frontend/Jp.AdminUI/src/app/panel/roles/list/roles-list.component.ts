import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "../../../core/translator/translator.service";
import { RoleService } from "../../../shared/services/role.service";
import { Role } from "../../../shared/viewModel/role.model";

const swal = require('sweetalert');

@Component({
    selector: "app-roles-list",
    templateUrl: "./roles-list.component.html",
    styleUrls: ["./roles-list.component.scss"],
    providers: [RoleService]
})
export class RolesListComponent implements OnInit {

    public roles: Role[];

    constructor(
        public translator: TranslatorService,
        private roleService: RoleService) { }

    ngOnInit() {
        this.loadGrants();
    }

    public loadGrants() {
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

                    this.roleService.remove(name).subscribe(
                        registerResult => {
                            if (registerResult.data) {
                                this.loadGrants();
                                swal("Deleted!", m["deleted"], 'success');
                            }
                        },
                        err => {
                            swal("Cancelled", "Unknown error while trying to remove", 'error');
                        }
                    );


                } else {
                    swal("Cancelled", m["cancelled"], 'error');
                }
            });
        });
    }

}
