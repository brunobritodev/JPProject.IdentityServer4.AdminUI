import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "../../../core/translator/translator.service";
import { ClientService } from "../clients.service";
import { flatMap } from "rxjs/operators";
import { Client } from "../../../shared/viewModel/client.model";
import { ActivatedRoute } from "@angular/router";


@Component({
    selector: "app-client-edit",
    templateUrl: "./client-edit.component.html",
    styleUrls: ["./client-edit.component.scss"],
    providers: [ClientService]
})
export class ClientEditComponent implements OnInit {

    public model: Client;
    constructor(
        private route: ActivatedRoute,
        public translator: TranslatorService,
        private clientService: ClientService) { }

    ngOnInit() {
        this.route.params.pipe(flatMap(p => this.clientService.getClientDetails(p["clientId"]))).subscribe(result => this.model = result.data);
    }

}
