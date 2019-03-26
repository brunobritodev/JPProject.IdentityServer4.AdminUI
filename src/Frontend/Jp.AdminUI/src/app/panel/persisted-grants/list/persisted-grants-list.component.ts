import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";
import { PersistedGrantsService } from "../persisted-grants.service";
import { PersistedGrant } from "@shared/viewModel/persisted-grants.model";
import { DefaultResponse } from "@shared/viewModel/default-response.model";

const swal = require('sweetalert');

@Component({
    selector: "app-persisted-grants-list",
    templateUrl: "./persisted-grants-list.component.html",
    styleUrls: ["./persisted-grants-list.component.scss"],
    providers: [PersistedGrantsService]
})
export class PersistedGrantListComponent implements OnInit {

    public persistedGrants: PersistedGrant[];
    grantDetail: PersistedGrant;

    public total: number;
    public page: number = 1;
    public quantity: number = 10;
    constructor(
        public translator: TranslatorService,
        private persistedGrantService: PersistedGrantsService) { }

    ngOnInit() {
        this.loadGrants();
    }

    public loadGrants() {
        this.persistedGrantService.getPersistedGrants(this.quantity, this.page).subscribe(a => {
            this.persistedGrants = a.data.persistedGrants;
            this.total = a.data.total;
            this.persistedGrants.forEach(grant => grant.parsedData = JSON.parse(grant.data));
        });
    }

    public remove(name: string) {
        this.translator.translate.get('persistedGrant.remove').subscribe(m => {
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

                    this.persistedGrantService.remove(name).subscribe(
                        registerResult => {
                            if (registerResult.data) {
                                this.loadGrants();
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

    public getData(data: string) {
        return JSON.parse(data);
    }

    public details(id: string) {
        this.grantDetail = this.persistedGrants.find(f => f.key == id);
    }
}
