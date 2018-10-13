import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";
import { ActivatedRoute, Router } from "@angular/router";
import { ToasterConfig, ToasterService } from "angular2-toaster";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { Observable } from "rxjs";
import { ApiResourceService } from "../api-resource.service";
import { StandardClaims } from "@shared/viewModel/standard-claims.model";
import { ApiResource } from "@shared/viewModel/api-resource.model";


@Component({
    selector: "app-api-resource-add",
    templateUrl: "./api-resource-add.component.html",
    styleUrls: ["./api-resource-add.component.scss"],
    providers: [ApiResourceService]
})
export class ApiResourceAddComponent implements OnInit {

    public errors: Array<string>;
    public model: ApiResource;
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
        private apiResourceService: ApiResourceService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.model = new ApiResource();
        this.errors = [];
        this.showButtonLoading = false;
        this.standardClaims = StandardClaims.claims;
    }

    public save() {
        this.errors = [];
        this.showButtonLoading = true;
        try {

            this.apiResourceService.save(this.model).subscribe(
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
            this.errors.push("Unknown error while trying to register");
            this.showButtonLoading = false;
            return Observable.throw("Unknown error while trying to register");
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
