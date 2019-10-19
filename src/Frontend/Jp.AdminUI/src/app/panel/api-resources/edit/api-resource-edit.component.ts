import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslatorService } from '@core/translator/translator.service';
import { ApiResource } from '@shared/viewModel/api-resource.model';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { StandardClaims } from '@shared/viewModel/standard-claims.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';
import * as jsonpatch from 'fast-json-patch';
import { flatMap, tap } from 'rxjs/operators';

import { ApiResourceService } from '../api-resource.service';

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
    patchObserver: jsonpatch.Observer<ApiResource>;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private apiResourceService: ApiResourceService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.route.params.pipe(
                tap(p => this.resourceId = p["name"]),
                flatMap(p => this.apiResourceService.getApiResourceDetails(p["name"])),
                tap(resource => this.patchObserver = jsonpatch.observe(resource)))
            .subscribe(result => this.model = result);
        this.errors = [];
        this.showButtonLoading = false;
        this.standardClaims = StandardClaims.claims;
    }

    public update() {
        this.showButtonLoading = true;
        this.errors = [];
        this.apiResourceService.partialUpdate(this.resourceId, jsonpatch.generate(this.patchObserver)).subscribe(
            () => {
                this.showSuccessMessage();
                this.showButtonLoading = false;
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
