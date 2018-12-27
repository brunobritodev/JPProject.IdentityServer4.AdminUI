import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";
import { flatMap, tap, map } from "rxjs/operators";
import { ActivatedRoute, Router } from "@angular/router";
import { ToasterConfig, ToasterService } from "angular2-toaster";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { Observable } from "rxjs";
import { ApiResourceService } from "../api-resource.service";
import { ApiResourceSecret } from "@shared/viewModel/api-resource.model";


@Component({
    selector: "app-api-resource-secrets",
    templateUrl: "./api-secrets.component.html",
    styleUrls: ["./api-secrets.component.scss"],
    providers: [ApiResourceService],
    encapsulation: ViewEncapsulation.None
})
export class ApiResourceSecretsComponent implements OnInit {

    public errors: Array<string>;

    public model: ApiResourceSecret;
    public apiSecrets: ApiResourceSecret[];

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public showButtonLoading: boolean;
    public resourceName: string;
    public hashTypes: { id: number; text: string; }[];
    public secretTypes: { id: string; text: string; }[];
    public bsConfig = {
        containerClass: 'theme-angle'
    };

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private apiResourceService: ApiResourceService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.route.params.pipe(tap(p => this.resourceName = p["resource"])).pipe(map(p => p["resource"])).pipe(flatMap(m => this.apiResourceService.getSecrets(m.toString()))).subscribe(result => this.apiSecrets = result.data);
        this.errors = [];
        this.model = new ApiResourceSecret();
        this.showButtonLoading = false;
        this.hashTypes = [{ id: 0, text: "Sha256" }, { id: 1, text: "Sha512" }];
        this.secretTypes = [{ id: 'SharedSecret', text: "Shared Secret" }, { id: 'X509Thumbprint', text: "X509 Thumbprint" }, { id: 'X509Name', text: "X509 Name" }, { id: 'X509CertificateBase64', text: "X509 Certificate Base64" }];
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

            this.apiResourceService.removeSecret(this.resourceName, id).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.loadSecrets();
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

    private loadSecrets(): void {
        this.apiResourceService.getSecrets(this.resourceName).subscribe(c => this.apiSecrets = c.data);
    }

    public save() {
        this.showButtonLoading = true;
        this.errors = [];
        try {
            this.model.resourceName = this.resourceName;
            this.apiResourceService.saveSecret(this.model).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.loadSecrets();
                        this.model = new ApiResourceSecret();
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
            this.errors.push("Unknown error while trying to register");
            this.showButtonLoading = false;
            return Observable.throw("Unknown error while trying to register");
        }
    }

}
