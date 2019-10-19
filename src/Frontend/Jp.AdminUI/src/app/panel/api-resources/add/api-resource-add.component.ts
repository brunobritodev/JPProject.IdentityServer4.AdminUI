import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslatorService } from '@core/translator/translator.service';
import { ApiResource } from '@shared/viewModel/api-resource.model';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { StandardClaims } from '@shared/viewModel/standard-claims.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';

import { ApiResourceService } from '../api-resource.service';


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
        this.apiResourceService.save(this.model).subscribe(
            registerResult => {
                this.showSuccessMessage();
                this.router.navigate(["/api-resource"]);
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );
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
