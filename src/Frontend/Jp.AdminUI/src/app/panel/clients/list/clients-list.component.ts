import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "../../../core/translator/translator.service";
import { ClientService } from "../clients.service";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { ClientList } from "../../../shared/viewModel/client-list.model";
const swal = require('sweetalert');

@Component({
    selector: "app-clients-list",
    templateUrl: "./clients-list.component.html",
    styleUrls: ["./clients-list.component.scss"],
    providers: [ClientService]
})
export class ClientListComponent implements OnInit {

    public clientList: ClientList[];

    constructor(
        public translator: TranslatorService,
        private clientService: ClientService) { }

    ngOnInit() {
        this.loadClients();
    }

    public loadClients() {
        this.clientService.getClients().subscribe(a => this.clientList = a.data);
    }

    public copy(clientId: string) {
        this.translator.translate.get('client.clone').subscribe(m => {
            swal({
                title: m['title'],
                text: m["text"],
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#b82d8',
                confirmButtonText: m["confirmButtonText"],
                cancelButtonText: m["cancelButtonText"],
                closeOnConfirm: false,
                closeOnCancel: false
            }, (isConfirm) => {
                if (isConfirm) {

                    this.clientService.copy(clientId).subscribe(
                        registerResult => {
                            if (registerResult.data) {
                                this.loadClients();
                                swal("Cloned!", m["cloned"], 'success');
                            }
                        },
                        err => {
                            swal("Error!", "Unknown error while trying to clone", 'error');
                        }
                    );
                } else {
                    swal("Cancelled", m["cancelled"], 'info');
                }
            });
        });
    }

    public remove(clientId: string) {
        this.translator.translate.get('client.remove').subscribe(m => {
            swal({
                title: m['title'],
                text: m["text"],
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: m["confirmButtonText"],
                cancelButtonText: m["cancelButtonText"],
                closeOnConfirm: false,
                closeOnCancel: false
            }, (isConfirm) => {
                if (isConfirm) {

                    this.clientService.remove(clientId).subscribe(
                        registerResult => {
                            if (registerResult.data) {
                                this.loadClients();
                                swal("Deleted!", m["deleted"], 'success');
                            }
                        },
                        err => {
                            swal("Cancelled", "Unknown error while trying to register", 'error');
                        }
                    );


                } else {
                    swal("Cancelled", m["cancelled"], 'error');
                }
            });
        });


    }
}
