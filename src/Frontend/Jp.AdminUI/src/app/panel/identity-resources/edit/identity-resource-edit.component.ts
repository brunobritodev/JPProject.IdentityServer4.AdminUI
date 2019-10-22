import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslatorService } from '@core/translator/translator.service';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { IdentityResource } from '@shared/viewModel/identity-resource.model';
import { StandardClaims } from '@shared/viewModel/standard-claims.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';
import * as jsonpatch from 'fast-json-patch';
import { flatMap, tap } from 'rxjs/operators';

import { IdentityResourceService } from '../identity-resource.service';


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
    public name: string;
    patchObserver: jsonpatch.Observer<IdentityResource>;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private identityResourceService: IdentityResourceService,
        public toasterService: ToasterService) { }

    public ngOnInit() {

        this.route.params
            .pipe(tap(p => this.name = p["name"]))
            .pipe(
                flatMap(p => this.identityResourceService.getIdentityResourceDetails(p["name"])),
                tap(resource => this.patchObserver = jsonpatch.observe(resource)
            ))
            .subscribe(result => this.model = result);
        this.errors = [];
        this.showButtonLoading = false;
        this.standardClaims = StandardClaims.claims;
    }

    public update() {

        this.showButtonLoading = true;
        this.errors = [];
        this.identityResourceService.partialUpdate(this.name, jsonpatch.generate(this.patchObserver)).subscribe(
            () => {
                this.updateCurrentResourceId();
                this.showSuccessMessage();
                this.showButtonLoading = false;
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );
    }
    updateCurrentResourceId() {
        this.name = this.model.name;
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
