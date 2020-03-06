import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from '@app/clients/clients.service';
import { TranslatorService } from '@core/translator/translator.service';
import { ClientProperty } from '@shared/viewModel/client.model';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';
import { Observable } from 'rxjs';
import { flatMap, map, tap } from 'rxjs/operators';


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
        this.route.params
            .pipe(tap(p => this.client = p["clientId"]))
            .pipe(map(p => p["clientId"]))
            .pipe(flatMap(m => this.clientService.getClientProperties(m.toString())))
            .subscribe(result => this.clientProperties = result);

        this.errors = [];
        this.model = new ClientProperty();
        this.showButtonLoading = false;
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

    public remove(key: string) {

        this.showButtonLoading = true;
        this.clientService.removeProperty(this.client, key).subscribe(
            () => {
                this.showSuccessMessage();
                this.loadProperties();
                this.showButtonLoading = false;
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );

    }

    private loadProperties(): void {
        this.clientService.getClientProperties(this.client).subscribe(c => this.clientProperties = c);
    }

    public save() {
        this.showButtonLoading = true;

        this.model.clientId = this.client;
        this.clientService.saveProperty(this.model).subscribe(
            properties => {
                this.showSuccessMessage();
                this.clientProperties = properties;
                this.model = new ClientProperty();
                this.showButtonLoading = false;
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );

    }

}
