import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "../../../core/translator/translator.service";
import { IdentityResourceService } from "../identity-resource.service";
import { IdentityResource } from "../../../shared/viewModel/identity-resource.model";
const swal = require('sweetalert');

@Component({
    selector: "app-identity-resources-list",
    templateUrl: "./identity-resources-list.component.html",
    styleUrls: ["./identity-resources-list.component.scss"],
    providers: [IdentityResourceService]
})
export class IdentityResourceListComponent implements OnInit {

    public identityResources: IdentityResource[];

    constructor(
        public translator: TranslatorService,
        private identityResourceService: IdentityResourceService) { }

    ngOnInit() {
        this.loadClients();
    }

    public loadClients() {
        this.identityResourceService.getIdentityResources().subscribe(a => this.identityResources = a.data);
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

                    this.identityResourceService.remove(clientId).subscribe(
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
