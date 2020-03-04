import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from '@app/clients/clients.service';
import { TranslatorService } from '@core/translator/translator.service';
import { ClientSecret } from '@shared/viewModel/client.model';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';
import { Observable } from 'rxjs';
import { flatMap, map, tap } from 'rxjs/operators';


@Component({
    selector: "app-client-secrets",
    templateUrl: "./secrets.component.html",
    styleUrls: ["./secrets.component.scss"],
    encapsulation: ViewEncapsulation.None
})
export class ClientSecretsComponent implements OnInit {

    public errors: Array<string>;

    public model: ClientSecret;
    public clientSecrets: ClientSecret[];

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public showButtonLoading: boolean;
    public client: string;
    public hashTypes: { id: number; text: string; }[];
    public secretTypes: { id: string; text: string; }[];
    public bsConfig = {
        containerClass: 'theme-angle'
    };

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private clientService: ClientService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.route.params
            .pipe(tap(p => this.client = p["clientId"]))
            .pipe(map(p => p["clientId"]))
            .pipe(flatMap(m => this.clientService.getClientSecrets(m.toString())))
            .subscribe(result => this.clientSecrets = result);

        this.errors = [];
        this.model = new ClientSecret();
        this.showButtonLoading = false;
        this.hashTypes = [{ id: 0, text: "Sha256" }, { id: 1, text: "Sha512" }];
        this.secretTypes = [{ id: 'SharedSecret', text: "Shared Secret" }, { id: 'X509Thumbprint', text: "X509 Thumbprint" }, { id: 'X509Name', text: "X509 Name" }, { id: 'X509CertificateBase64', text: "X509 Certificate Base64" }];
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

    public remove(secret: ClientSecret) {

        this.showButtonLoading = true;
        this.errors = [];
        this.clientService.removeSecret(this.client, secret.type, secret.value).subscribe(
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
        this.clientService.getClientSecrets(this.client).subscribe(c => this.clientSecrets = c);
    }

    public save() {
        this.showButtonLoading = true;
        this.errors = [];

        this.model.clientId = this.client;
        this.clientService.saveSecret(this.model).subscribe(
            registerResult => {
                this.showSuccessMessage();
                this.clientSecrets = registerResult;
                this.model = new ClientSecret();
                this.showButtonLoading = false;
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );

    }

}
