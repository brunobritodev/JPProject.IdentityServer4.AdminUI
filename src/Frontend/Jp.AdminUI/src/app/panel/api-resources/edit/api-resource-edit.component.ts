import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";
import { flatMap, tap } from "rxjs/operators";
import { ActivatedRoute, Router } from "@angular/router";
import { ToasterConfig, ToasterService } from "angular2-toaster";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { Observable } from "rxjs";
import { ApiResourceService } from "../api-resource.service";
import { ApiResource } from "@shared/viewModel/api-resource.model";
import { StandardClaims } from "@shared/viewModel/standard-claims.model";


@Component({
    selector: "app-api-resource-edit",
    templateUrl: "./api-resource-edit.component.html",
    styleUrls: ["./api-resource-edit.component.scss"],
    providers: [ApiResourceService]
})
export class ApiResourceEditComponent implements OnInit {

    public errors: Array<string>;
    public model: ApiResource;
    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public showButtonLoading: boolean;
    public resourceId: string;
    standardClaims: string[];

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private apiResourceService: ApiResourceService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.route.params.pipe(tap(p => this.resourceId = p["name"])).pipe(flatMap(p => this.apiResourceService.getApiResourceDetails(p["name"]))).subscribe(result => this.model = result.data);
        this.errors = [];
        this.showButtonLoading = false;
        this.standardClaims = StandardClaims.claims;
    }

    public update() {
        this.showButtonLoading = true;
        this.errors = [];
        try {
            this.model.oldApiResourceId = this.resourceId;
            this.apiResourceService.update(this.model).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.router.navigate(["/api-resource"]);
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
