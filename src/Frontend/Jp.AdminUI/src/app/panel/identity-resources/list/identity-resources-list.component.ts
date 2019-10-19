import { Component, OnInit } from '@angular/core';
import { TranslatorService } from '@core/translator/translator.service';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { IdentityResource } from '@shared/viewModel/identity-resource.model';
import Swal from 'sweetalert2';

import { IdentityResourceService } from '../identity-resource.service';

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
        this.identityResourceService.getIdentityResources().subscribe(a => this.identityResources = a);
    }

    public remove(name: string) {
        this.translator.translate.get('identityResource.remove').subscribe(m => {
            Swal.fire({
                title: m['title'],
                text: m["text"],
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: m["confirmButtonText"],
                cancelButtonText: m["cancelButtonText"],

            }).then(isConfirm => {
                if (isConfirm) {

                    this.identityResourceService.remove(name).subscribe(
                        () => {
                            this.loadResources();
                            Swal.fire("Deleted!", m["deleted"], 'success');
                        },
                        err => {
                            let errors = ProblemDetails.GetErrors(err).map(a => a.value);
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
