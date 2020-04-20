import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientService } from '@app/clients/clients.service';
import { TranslatorService } from '@core/translator/translator.service';
import { Client } from '@shared/viewModel/client.model';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { ToasterConfig, ToasterService } from 'angular2-toaster';
import * as jsonpatch from 'fast-json-patch';
import { flatMap, tap } from 'rxjs/operators';


@Component({
    selector: "app-client-edit",
    templateUrl: "./client-edit.component.html",
    styleUrls: ["./client-edit.component.scss"],
})
export class ClientEditComponent implements OnInit {

    public errors: Array<string>;
    public model: Client;
    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true,
        timeout: 2000
    });
    public showButtonLoading: boolean;
    public clientId: string;
    patchObserver: jsonpatch.Observer<Client>;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private clientService: ClientService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.route.params.pipe(
                tap(p => this.clientId = p["clientId"]),
                flatMap(p => this.clientService.getClientDetails(p["clientId"])),
                tap(client => this.patchObserver = jsonpatch.observe(client))
                )
            .subscribe(result => this.model = result);
        this.errors = [];
        this.showButtonLoading = false;
    }

    public update() {
        if (!Client.isValid(this.model, this.errors))
            return;

        this.showButtonLoading = true;
        this.errors = [];
        
        this.clientService.update(this.clientId, this.model).subscribe(
            () => {
                this.updateCurrentClientId();
                this.showSuccessMessage();
                this.showButtonLoading = false;
            },
            err => {
                this.errors = ProblemDetails.GetErrors(err).map(a => a.value);
                this.showButtonLoading = false;
            }
        );

    }

    private updateCurrentClientId() {
        this.clientId = this.model.clientId;
    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

}
