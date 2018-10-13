import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";
import { IdentityResourceService } from "../identity-resource.service";
import { IdentityResource } from "@shared/viewModel/identity-resource.model";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
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
        this.loadResources();
    }

    public loadResources() {
        this.identityResourceService.getIdentityResources().subscribe(a => this.identityResources = a.data);
    }

    public remove(name: string) {
        this.translator.translate.get('identityResource.remove').subscribe(m => {
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
                                this.loadResources();
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
