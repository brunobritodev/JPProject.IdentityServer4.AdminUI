import { Component, OnInit, ViewEncapsulation } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";
import { flatMap, tap, map } from "rxjs/operators";
import { ClientService } from "@app/clients/clients.service";
import {  ClientProperty } from "@shared/viewModel/client.model";
import { ActivatedRoute, Router } from "@angular/router";
import { ToasterConfig, ToasterService } from "angular2-toaster";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { Observable } from "rxjs";


@Component({
    selector: "app-client-properties",
    templateUrl: "./properties.component.html",
    styleUrls: ["./properties.component.scss"],
    encapsulation: ViewEncapsulation.None
})
export class ClientPropertiesComponent implements OnInit {

    public errors: Array<string>;

    public model: ClientProperty;
    public clientProperties: ClientProperty[];

    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public showButtonLoading: boolean;
    public client: string;
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
        this.route.params.pipe(tap(p => this.client = p["clientId"])).pipe(map(p => p["clientId"])).pipe(flatMap(m => this.clientService.getClientProperties(m.toString()))).subscribe(result => this.clientProperties = result.data);
        this.errors = [];
        this.model = new ClientProperty();
        this.showButtonLoading = false;
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

    public remove(id: number) {

        this.showButtonLoading = true;
        try {

            this.clientService.removeProperty(this.client, id).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.loadProperties();
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

    private loadProperties(): void {
        this.clientService.getClientProperties(this.client).subscribe(c => this.clientProperties = c.data);
    }

    public save() {
        this.showButtonLoading = true;
        try {
            this.model.clientId = this.client;
            this.clientService.saveProperty(this.model).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.loadProperties();
                        this.model = new ClientProperty();
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
