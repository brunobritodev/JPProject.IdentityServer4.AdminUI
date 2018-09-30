import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "../../../core/translator/translator.service";
import { IdentityResource } from "../../../shared/viewModel/identity-resource.model";
import { ApiResourceService } from "../api-resource.service";
const swal = require('sweetalert');

@Component({
    selector: "app-api-resources-list",
    templateUrl: "./api-resources-list.component.html",
    styleUrls: ["./api-resources-list.component.scss"],
    providers: [ApiResourceService]
})
export class ApiResourceListComponent implements OnInit {

    public identityResources: IdentityResource[];

    constructor(
        public translator: TranslatorService,
        private identityResourceService: ApiResourceService) { }

    ngOnInit() {
        this.loadClients();
    }

    public loadClients() {
        this.identityResourceService.getIdentityResources().subscribe(a => this.identityResources = a.data);
    }

    public remove(name: string) {
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

                    this.identityResourceService.remove(name).subscribe(
                        registerResult => {
                            if (registerResult.data) {
                                this.loadClients();
                                swal("Deleted!", m["deleted"], 'success');
                            }
                        },
                        err => {
                            swal("Cancelled", "Unknown error while trying to remove", 'error');
                        }
                    );


                } else {
                    swal("Cancelled", m["cancelled"], 'error');
                }
            });
        });


    }
}
