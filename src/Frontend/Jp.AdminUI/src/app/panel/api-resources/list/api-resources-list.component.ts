import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "../../../core/translator/translator.service";
import { ApiResourceService } from "../api-resource.service";
import { ApiResource } from "../../../shared/viewModel/api-resource.model";
const swal = require('sweetalert');

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
        this.apiResourceservice.getApiResources().subscribe(a => this.apiResources = a.data);
    }

    public remove(name: string) {
        this.translator.translate.get('apiResource.remove').subscribe(m => {
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

                    this.apiResourceservice.remove(name).subscribe(
                        registerResult => {
                            if (registerResult.data) {
                                this.loadResources();
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
