import { Component, OnInit } from "@angular/core";
import { TranslatorService } from "@core/translator/translator.service";
import { UserService } from "@shared/services/user.service";
import { UserProfile, ListOfUsers } from "@shared/viewModel/userProfile.model";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { Observable, Subject } from "rxjs";
import { EventHistoryData } from "@shared/viewModel/event-history-data.model";
import { debounceTime, switchMap } from "rxjs/operators";
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

    public total: number;
    public page: number = 1;
    public quantity: number = 10;
    private userSearch: Subject<string> = new Subject<string>();

    constructor(
        public translator: TranslatorService,
        private userService: UserService) { }

    ngOnInit() {
        this.loadResources();
        this.userSearch
            .pipe(debounceTime(500))
            .pipe(switchMap(a => this.userService.findUsers(a, this.quantity, this.page)))
            .subscribe((response: DefaultResponse<ListOfUsers>) => {
                this.users = response.data.users;
                this.total = response.data.total;
            });
    }

    public loadResources() {
        this.userService.getUsers(this.quantity, this.page).subscribe(a => {
            this.users = a.data.users;
            this.total = a.data.total;
        });
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


    public findUser(event: any) {
        if (event.target.value == null || event.target.value === "") {
            this.loadResources();
        }

        this.userSearch.next(event.target.value);
    }

}
