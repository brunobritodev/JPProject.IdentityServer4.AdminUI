import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";
import { flatMap, tap, map } from "rxjs/operators";
import { ClientService } from "@app/clients/clients.service";
import { ClientSecret } from "@shared/viewModel/client.model";
import { ActivatedRoute, Router } from "@angular/router";
import { ToasterConfig, ToasterService } from "angular2-toaster";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { Observable } from "rxjs";


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
        this.route.params.pipe(tap(p => this.client = p["clientId"])).pipe(map(p => p["clientId"])).pipe(flatMap(m => this.clientService.getClientSecrets(m.toString()))).subscribe(result => this.clientSecrets = result.data);
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

    public remove(id: number) {

        this.showButtonLoading = true;
        this.errors = [];
        try {

            this.clientService.removeSecret(this.client, id).subscribe(
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

        this.clientService.getClientSecrets(this.client).subscribe(c => this.clientSecrets = c.data);
    }

    public save() {
        this.showButtonLoading = true;
        this.errors = [];
        try {
            this.model.clientId = this.client;
            this.clientService.saveSecret(this.model).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.loadSecrets();
                        this.model = new ClientSecret();
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
