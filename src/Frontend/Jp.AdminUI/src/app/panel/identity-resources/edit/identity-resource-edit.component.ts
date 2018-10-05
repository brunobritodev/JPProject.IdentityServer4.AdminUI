import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "../../../core/translator/translator.service";
import { flatMap } from "rxjs/operators";
import { ActivatedRoute, Router } from "@angular/router";
import { ToasterConfig, ToasterService } from "angular2-toaster";
import { DefaultResponse } from "../../../shared/viewModel/default-response.model";
import { Observable } from "rxjs";
import { IdentityResourceService } from "../identity-resource.service";
import { IdentityResource } from "../../../shared/viewModel/identity-resource.model";
import { StandardClaims } from "../../../shared/viewModel/standard-claims.model";


@Component({
    selector: "app-identity-resource-edit",
    templateUrl: "./identity-resource-edit.component.html",
    styleUrls: ["./identity-resource-edit.component.scss"],
    providers: [IdentityResourceService]
})
export class IdentityResourceEditComponent implements OnInit {

    public errors: Array<string>;
    public model: IdentityResource;
    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public showButtonLoading: boolean;
    standardClaims: string[];

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private identityResourceService: IdentityResourceService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.route.params.pipe(flatMap(p => this.identityResourceService.getIdentityResourceDetails(p["name"]))).subscribe(result => this.model = result.data);
        this.errors = [];
        this.showButtonLoading = false;
        this.standardClaims = StandardClaims.claims;
    }

    public update() {

        this.showButtonLoading = true;
        this.errors = [];
        try {

            this.identityResourceService.update(this.model).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.router.navigate(["/identity-resource"]);
                    }
                },
                err => {
                    this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                    if (this.errors[0] == undefined) {
                        this.errors = [];
                        this.errors.push("Unknown error while trying to update");
                    }
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

    public addClaim(claim: string) {
        if (this.model.userClaims.find(f => f == claim) == null)
            this.model.userClaims.push(claim);
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }
}
