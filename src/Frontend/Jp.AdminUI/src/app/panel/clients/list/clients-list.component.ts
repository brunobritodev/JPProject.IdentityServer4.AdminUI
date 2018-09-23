import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "../../../core/translator/translator.service";
import { ClientService } from "../clients.service";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { ClientList } from "../../../shared/viewModel/client-list.model";


@Component({
    selector: "app-clients-list",
    templateUrl: "./clients-list.component.html",
    styleUrls: ["./clients-list.component.scss"],
    providers: [ClientService]
})
export class ClientListComponent implements OnInit {

    public clientListObservable: Observable<ClientList[]>;

    constructor(
        public translator: TranslatorService,
        private clientService: ClientService) { }

    ngOnInit() {
        this.clientListObservable = this.clientService.getClients().pipe(map(a => a.data));
    }

}
