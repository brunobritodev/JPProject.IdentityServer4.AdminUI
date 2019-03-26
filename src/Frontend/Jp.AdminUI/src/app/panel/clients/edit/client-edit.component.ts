import { Component, OnInit } from "@angular/core";
import { ClientService } from "@app/clients/clients.service";
import { flatMap, tap } from "rxjs/operators";
import { Client } from "@shared/viewModel/client.model";
import { ActivatedRoute, Router } from "@angular/router";
import { ToasterConfig, ToasterService } from "angular2-toaster";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { Observable } from "rxjs";
import { TranslatorService } from "@core/translator/translator.service";


@Component({
    selector: "app-client-edit",
    templateUrl: "./client-edit.component.html",
    styleUrls: ["./client-edit.component.scss"]
})
export class ClientEditComponent implements OnInit {

    public errors: Array<string>;
    public model: Client;
    public toasterconfig: ToasterConfig = new ToasterConfig({
        positionClass: 'toast-top-right',
        showCloseButton: true
    });
    public showButtonLoading: boolean;
    public clientId: string;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        public translator: TranslatorService,
        private clientService: ClientService,
        public toasterService: ToasterService) { }

    public ngOnInit() {
        this.route.params.pipe(tap(p => this.clientId = p["clientId"])).pipe(flatMap(p => this.clientService.getClientDetails(p["clientId"]))).subscribe(result => this.model = result.data);
        this.errors = [];
        this.showButtonLoading = false;
    }

    public update() {
        if (!Client.isValid(this.model, this.errors))
            return;

        this.showButtonLoading = true;
        this.errors = [];
        try {
            this.model.oldClientId = this.clientId;
            this.clientService.update(this.model).subscribe(
                registerResult => {
                    if (registerResult.data) {
                        this.showSuccessMessage();
                        this.router.navigate(["/clients"]);
                    }
                },
                err => {
                    this.errors = DefaultResponse.GetErrors(err).map(a => a.value);
                    this.showButtonLoading = false;
                }
            );
        } catch (error) {
            this.errors = [];
            this.errors.push("Unknown error while trying to update");
            this.showButtonLoading = false;
            return Observable.throw("Unknown error while trying to update");
        }

    }

    public showSuccessMessage() {
        this.translator.translate.get('toasterMessages').subscribe(a => {
            this.toasterService.pop("success", a["title-success"], a["message-success"]);
        });
    }

}
