import { Component, OnInit } from '@angular/core';
import { SettingsService } from '@core/settings/settings.service';
import { TranslatorService } from '@core/translator/translator.service';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { PersistedGrant } from '@shared/viewModel/persisted-grants.model';
import Swal from 'sweetalert2';

import { PersistedGrantsService } from '../persisted-grants.service';

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
        private persistedGrantService: PersistedGrantsService,
        public settings: SettingsService) { }

    ngOnInit() {
        this.loadGrants();
    }

    public loadGrants() {
        this.persistedGrantService.getPersistedGrants(this.quantity, this.page).subscribe(a => {
            this.persistedGrants = a.collection;
            this.total = a.total;
            this.persistedGrants.forEach(grant => grant.parsedData = JSON.parse(grant.data));
        });
    }

    public remove(name: string) {
        this.translator.translate.get('persistedGrant.remove').subscribe(m => {
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

                    this.persistedGrantService.remove(name).subscribe(
                        () => {
                            this.loadGrants();
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

    public getData(data: string) {
        return JSON.parse(data);
    }

    public details(id: string) {
        this.grantDetail = this.persistedGrants.find(f => f.key == id);
    }
}
