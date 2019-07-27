import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";
import { ClientService } from "@app/clients/clients.service";
import { ClientList } from "@shared/viewModel/client-list.model";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import Swal from 'sweetalert2'

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
            Swal.fire({
                title: m['title'],
                text: m["text"],
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#b82d8',
                confirmButtonText: m["confirmButtonText"],
                cancelButtonText: m["cancelButtonText"],
            }).then(isConfirm => {
                if (isConfirm.value) {

                    this.clientService.copy(clientId).subscribe(
                        registerResult => {
                            if (registerResult.data) {
                                this.loadClients();
                                Swal.fire("Cloned!", m["cloned"], 'success');
                            }
                        },
                        err => {
                            let errors = DefaultResponse.GetErrors(err).map(a => a.value);
                            Swal.fire("Error!", errors[0], 'error');
                        }
                    );
                } else {
                    Swal.fire("Cancelled", m["cancelled"], 'info');
                }
            });
        });
    }

    public remove(clientId: string) {
        this.translator.translate.get('client.remove').subscribe(m => {
            Swal.fire({
                title: m['title'],
                text: m["text"],
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: m["confirmButtonText"],
                cancelButtonText: m["cancelButtonText"]
            }).then(isConfirm => {
                if (isConfirm.value) {

                    this.clientService.remove(clientId).subscribe(
                        registerResult => {
                            if (registerResult.data) {
                                this.loadClients();
                                Swal.fire("Deleted!", m["deleted"], 'success');
                            }
                        },
                        err => {
                            let errors = DefaultResponse.GetErrors(err).map(a => a.value);
                            Swal.fire("Error!", errors[0], 'error');
                        }
                    );


                } else {
                    Swal.fire("Cancelled", m["cancelled"], 'error');
                }
            });
        });


    }
}
