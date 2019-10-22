import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslatorService } from '@core/translator/translator.service';
import { RoleService } from '@shared/services/role.service';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { Role } from '@shared/viewModel/role.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';

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
        this.roleService.save(this.model).subscribe(
            registerResult => {
                if (registerResult) {
                    this.showSuccessMessage();
                    this.router.navigate(["/roles"]);
                }
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            });

    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

}
