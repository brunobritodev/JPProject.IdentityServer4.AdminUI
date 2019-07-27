import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";
import { RoleService } from "@shared/services/role.service";
import { Role } from "@shared/viewModel/role.model";
import { ToasterConfig, ToasterService } from "angular2-toaster";
import { ActivatedRoute, Router } from "@angular/router";
import { flatMap, tap } from "rxjs/operators";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { Observable } from "rxjs";

import Swal from 'sweetalert2'

@Component({
    selector: "app-role-add",
    templateUrl: "./role-add.component.html",
    styleUrls: ["./role-add.component.scss"],
    providers: [RoleService]
})
export class RoleAddComponent implements OnInit {

    public errors: Array<string>;
    public model: Role;
    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public showButtonLoading: boolean;

    constructor(
        private router: Router,
        public translator: TranslatorService,
        private roleService: RoleService,
        public toasterService: ToasterService) { }


    ngOnInit() {
        this.errors = [];
        this.model = new Role();
        this.showButtonLoading = false;
    }

    public save() {
        this.showButtonLoading = true;
        this.errors = [];
        try {
            this.roleService.save(this.model).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.router.navigate(["/roles"]);
                    }
                },
                err => {
                    this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                    this.showButtonLoading = false;
                });
        } catch (error) {
            this.errors = [];
            this.errors.push("Unknown error while trying to update");
            this.showButtonLoading = false;
            return Observable.throw("Unknown error while trying to update");
        }

    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

}
