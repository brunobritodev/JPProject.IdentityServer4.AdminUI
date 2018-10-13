import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";
import { flatMap, tap, map } from "rxjs/operators";
import { ActivatedRoute, Router } from "@angular/router";
import { ToasterConfig, ToasterService } from "angular2-toaster";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { Observable } from "rxjs";
import { ApiResourceService } from "../api-resource.service";
import { Scope } from "@shared/viewModel/scope.model";
import { StandardClaims } from "@shared/viewModel/standard-claims.model";


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
        this.route.params.pipe(tap(p => this.resourceName = p["resource"])).pipe(map(p => p["resource"])).pipe(flatMap(m => this.apiResourceService.getScopes(m.toString()))).subscribe(result => this.apiScopes = result.data);
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

    public remove(id: number) {
        this.showButtonLoading = true;
        this.errors = [];
        try {

            this.apiResourceService.removeScope(this.resourceName, id).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.loadScopes();
                    }
                    this.showButtonLoading = false;
                },
                err => {
                    this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                    this.showButtonLoading = false;
                }
            );
        } catch (error) {
            this.errors = [];
            this.errors.push("Unknown error while trying to remove");
            this.showButtonLoading = false;
            return Observable.throw("Unknown error while trying to remove");
        }

    }

    private loadScopes(): void {
        this.apiResourceService.getScopes(this.resourceName).subscribe(c => this.apiScopes = c.data);
    }

    public save() {
        this.showButtonLoading = true;
        this.errors = [];
        try {
            this.model.resourceName = this.resourceName;
            this.apiResourceService.saveScope(this.model).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.loadScopes();
                        this.model = new Scope();
                    }
                    this.showButtonLoading = false;
                },
                err => {
                    this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                    this.showButtonLoading = false;
                }
            );
        } catch (error) {
            this.errors = [];
            this.errors.push("Unknown error while trying to save");
            this.showButtonLoading = false;
            return Observable.throw("Unknown error while trying to save");
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
