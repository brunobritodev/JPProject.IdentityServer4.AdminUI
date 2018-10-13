import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "../../../core/translator/translator.service";
import { UserService } from "../../../shared/services/user.service";
import { UserProfile } from "../../../shared/viewModel/userProfile.model";
import { DefaultResponse } from "../../../shared/viewModel/default-response.model";
import { Observable } from "rxjs";
import { EventHistoryData } from "../../../shared/viewModel/event-history-data.model";
const swal = require('sweetalert');

@Component({
    selector: "app-user-list",
    templateUrl: "./user-list.component.html",
    styleUrls: ["./user-list.component.scss"],
    providers: [UserService]
})
export class UserListComponent implements OnInit {

    public users: UserProfile[];
    public historyData$: Observable<EventHistoryData[]>;
    public selectedUser: UserProfile;

    constructor(
        public translator: TranslatorService,
        private userService: UserService) { }

    ngOnInit() {
        this.loadResources();
    }

    public loadResources() {
        this.userService.getUsers().subscribe(a => this.users = a.data);
    }

    public remove(id: string) {
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

                    this.userService.remove(id).subscribe(
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

    public showLogs(user: UserProfile) {
        this.selectedUser = user;
        this.historyData$ = this.userService.showLogs(user.userName);
    }

    parse(details: string) {
        return JSON.parse(details);
    }
}
