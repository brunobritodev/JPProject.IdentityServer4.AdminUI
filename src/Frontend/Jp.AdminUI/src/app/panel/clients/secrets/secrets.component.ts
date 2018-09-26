import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { TranslatorService } from "../../../core/translator/translator.service";
import { ClientService } from "../clients.service";
import { flatMap, tap, map } from "rxjs/operators";
import { Client, ClientSecret } from "../../../shared/viewModel/client.model";
import { ActivatedRoute, Router } from "@angular/router";
import { ToasterConfig, ToasterService } from "angular2-toaster";
import { DefaultResponse } from "../../../shared/viewModel/default-response.model";
import { Observable } from "rxjs";


@Component({
    selector: "app-client-secrets",
    templateUrl: "./secrets.component.html",
    styleUrls: ["./secrets.component.scss"],
    providers: [ClientService],
    encapsulation: ViewEncapsulation.None
})
export class ClientSecretsComponent implements OnInit {

    public errors: Array<string>;

    public model: ClientSecret;
    public clientSecretsObservable: Observable<ClientSecret[]>;

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public showButtonLoading: boolean;
    public client: string;
    public hashTypes: { id: number; text: string; }[];
    public secretTypes: { id: number; text: string; }[];
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
        this.clientSecretsObservable = this.route.params.pipe(tap(p => this.client = p["clientId"])).pipe(map(p => p["clientId"])).pipe(flatMap(m => this.clientService.getClientSecrets(m.toString()))).pipe(map(result => result.data));
        this.errors = [];
        this.model = new ClientSecret();
        this.showButtonLoading = false;
        this.hashTypes = [{ id: 0, text: "Sha256" }, { id: 1, text: "Sha512" }];
        this.secretTypes = [{ id: 0, text: "SharedSecret" }, { id: 1, text: "X509Thumbprint" }, { id: 2, text: "X509Name" }, { id: 3, text: "X509CertificateBase64" }];
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

    public remove(id: number) {

        this.showButtonLoading = true;
        try {

            this.clientService.removeSecret(this.client, id).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.router.navigate(["/client/edit", this.client]);
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



    public save() {
        this.showButtonLoading = true;
        try {
            this.model.clientId = this.client;
            this.clientService.saveSecret(this.model).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.clientSecretsObservable.subscribe();
                        this.model = new ClientSecret();
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

    public teste() {

    }

}
