import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";
import { ClientService } from "@app/clients/clients.service";
import { ClientList } from "@shared/viewModel/client-list.model";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
const swal = require('sweetalert');

@Component({
    selector: "app-clients-list",
    templateUrl: "./clients-list.component.html",
    styleUrls: ["./clients-list.component.scss"],
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
                            let errors = DefaultResponse.GetErrors(err).map(a => a.value);
                            swal("Error!", errors[0], 'error');
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
                            let errors = DefaultResponse.GetErrors(err).map(a => a.value);
                            swal("Error!", errors[0], 'error');
                        }
                    );


                } else {
                    swal("Cancelled", m["cancelled"], 'error');
                }
            });
        });


    }
}
