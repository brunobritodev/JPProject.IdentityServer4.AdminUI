import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";
import { RoleService } from "@shared/services/role.service";
import { Role } from "@shared/viewModel/role.model";
import { ToasterConfig, ToasterService } from "angular2-toaster";
import { ActivatedRoute, Router } from "@angular/router";
import { flatMap, tap } from "rxjs/operators";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { Observable } from "rxjs";

const swal = require('sweetalert');

@Component({
    selector: "app-role-edit",
    templateUrl: "./role-edit.component.html",
    styleUrls: ["./role-edit.component.scss"],
    providers: [RoleService]
})
export class RoleEditComponent implements OnInit {

    public errors: Array<string>;
    public model: Role;
    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public showButtonLoading: boolean;
    public role: string;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private roleService: RoleService,
        public toasterService: ToasterService) { }


    ngOnInit() {
        this.route.params.pipe(tap(p => this.role = p["role"])).pipe(flatMap(p => this.roleService.getRoleDetails(p["role"]))).subscribe(result => this.model = result.data);
        this.errors = [];
        this.showButtonLoading = false;
    }

    public update() {
        this.showButtonLoading = true;
        this.errors = [];
        try {
            this.model.oldName = this.role;
            this.roleService.update(this.model).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.router.navigate(["/roles"]);
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

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

}
