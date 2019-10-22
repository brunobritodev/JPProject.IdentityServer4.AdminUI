import { Component, OnInit } from '@angular/core';
import { TranslatorService } from '@core/translator/translator.service';
import { ApiResource } from '@shared/viewModel/api-resource.model';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import Swal from 'sweetalert2';

import { ApiResourceService } from '../api-resource.service';

@Component({
    selector: "app-api-resources-list",
    templateUrl: "./api-resources-list.component.html",
    styleUrls: ["./api-resources-list.component.scss"],
    providers: [ApiResourceService]
})
export class ApiResourceListComponent implements OnInit {

    public apiResources: ApiResource[];

    constructor(
        public translator: TranslatorService,
        private apiResourceservice: ApiResourceService) { }

    ngOnInit() {
        this.loadResources();
    }

    public loadResources() {
        this.apiResourceservice.getApiResources().subscribe(a => this.apiResources = a);
    }

    public remove(name: string) {
        this.translator.translate.get('apiResource.remove').subscribe(m => {
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

                    this.apiResourceservice.remove(name).subscribe(
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
