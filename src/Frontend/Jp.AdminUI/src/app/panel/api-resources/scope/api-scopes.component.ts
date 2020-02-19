import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslatorService } from '@core/translator/translator.service';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { Scope } from '@shared/viewModel/scope.model';
import { StandardClaims } from '@shared/viewModel/standard-claims.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';
import { throwError } from 'rxjs';
import { flatMap, map, tap } from 'rxjs/operators';

import { ApiResourceService } from '../api-resource.service';


@Component({
    selector: "app-api-resource-scopes",
    templateUrl: "./api-scopes.component.html",
    styleUrls: ["./api-scopes.component.scss"],
    providers: [ApiResourceService],
    encapsulation: ViewEncapsulation.None
})
export class ApiResourceScopesComponent implements OnInit {

    public errors: Array<string>;

    public model: Scope;
    public apiScopes: Scope[];

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public showButtonLoading: boolean;
    public resourceName: string;
    public bsConfig = {
        containerClass: 'theme-angle'
    };
    public standardClaims: string[];
    public selectedScope: Scope;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private apiResourceService: ApiResourceService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.route.params
            .pipe(tap(p => this.resourceName = p["resource"]))
            .pipe(map(p => p["resource"]))
            .pipe(flatMap(m => this.apiResourceService.getScopes(m.toString())))
            .subscribe(result => this.apiScopes = result);

        this.errors = [];
        this.model = new Scope();
        this.showButtonLoading = false;
        this.standardClaims = StandardClaims.claims;
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

    public remove(scope: Scope) {
        this.showButtonLoading = true;
        this.errors = [];
        this.apiResourceService.removeScope(this.resourceName, scope.name).subscribe(
            () => {
                this.showSuccessMessage();
                this.loadScopes();
                this.showButtonLoading = false;
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );
    }

    private loadScopes(): void {
        this.apiResourceService.getScopes(this.resourceName).subscribe(c => this.apiScopes = c);
    }

    public save() {
        this.showButtonLoading = true;
        this.errors = [];
        try {
            this.model.resourceName = this.resourceName;
            this.apiResourceService.saveScope(this.model).subscribe(
                scopes => {
                    this.showSuccessMessage();
                    this.apiScopes = scopes;
                    this.model = new Scope();
                    this.showButtonLoading = false;
                },
                err => {
                    this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                    this.showButtonLoading = false;
                }
            );
        } catch (error) {
            this.errors = [];
            this.errors.push("Unknown error while trying to save");
            this.showButtonLoading = false;
            return throwError("Unknown error while trying to save");
        }
    }

    public details(scope: Scope) {
        this.selectedScope = scope;
    }

    public addClaim(claim: string) {
        if (this.model.userClaims.find(f => f == claim) == null)
            this.model.userClaims.push(claim);
    }


}
