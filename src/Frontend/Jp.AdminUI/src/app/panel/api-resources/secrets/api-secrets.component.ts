import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslatorService } from '@core/translator/translator.service';
import { ApiResourceSecret } from '@shared/viewModel/api-resource.model';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';
import { Observable } from 'rxjs';
import { flatMap, map, tap } from 'rxjs/operators';

import { ApiResourceService } from '../api-resource.service';


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
        this.route.params.pipe(tap(p => this.resourceName = p["resource"])).pipe(map(p => p["resource"])).pipe(flatMap(m => this.apiResourceService.getSecrets(m.toString()))).subscribe(result => this.apiSecrets = result);
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

    public remove(secret: ApiResourceSecret) {
        this.showButtonLoading = true;
        this.errors = [];
        this.apiResourceService.removeSecret(this.resourceName, secret.type, secret.value).subscribe(
            () => {
                this.showSuccessMessage();
                this.loadSecrets();
                this.showButtonLoading = false;
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );

    }

    private loadSecrets(): void {
        this.apiResourceService.getSecrets(this.resourceName).subscribe(c => this.apiSecrets = c);
    }

    public save() {
        this.showButtonLoading = true;
        this.errors = [];
        this.model.resourceName = this.resourceName;
        this.apiResourceService.saveSecret(this.model).subscribe(
            secrets => {
                this.showSuccessMessage();
                this.apiSecrets = secrets;
                this.model = new ApiResourceSecret();
                this.showButtonLoading = false;
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );
    }

}
