import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "../../../core/translator/translator.service";
import { PersistedGrantsService } from "../persisted-grants.service";
import { PersistedGrant } from "../../../shared/viewModel/persisted-grants.model";

const swal = require('sweetalert');

@Component({
    selector: "app-persisted-grants-list",
    templateUrl: "./persisted-grants-list.component.html",
    styleUrls: ["./persisted-grants-list.component.scss"],
    providers: [PersistedGrantsService]
})
export class PersistedGrantListComponent implements OnInit {

    public persistedGrants: PersistedGrant[];

    constructor(
        public translator: TranslatorService,
        private persistedGrantService: PersistedGrantsService) { }

    ngOnInit() {
        this.loadGrants();
    }

    public loadGrants() {
        this.persistedGrantService.getPersistedGrants().subscribe(a => this.persistedGrants = a.data);
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
