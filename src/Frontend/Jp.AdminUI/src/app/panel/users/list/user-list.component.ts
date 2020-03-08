import { Component, OnInit } from '@angular/core';
import { TranslatorService } from '@core/translator/translator.service';
import { UserService } from '@shared/services/user.service';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { EventHistoryData } from '@shared/viewModel/event-history-data.model';
import { ListOfUsers, UserProfile } from '@shared/viewModel/userProfile.model';
import { Observable, pipe, Subject } from 'rxjs';
import { debounceTime, switchMap, tap } from 'rxjs/operators';
import Swal from 'sweetalert2';

@Component({
    selector: "app-user-list",
    templateUrl: "./user-list.component.html",
    styleUrls: ["./user-list.component.scss"],
    providers: [UserService]
})
export class UserListComponent implements OnInit {

    public users: UserProfile[];
    public selectedUser: UserProfile;
    public loading: boolean = true;
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
            .pipe(tap(() => this.animateLoadUsers()))
            .pipe(switchMap(a => this.userService.findUsers(a, this.quantity, this.page)))
            .subscribe((response: ListOfUsers) => {
                this.users = response.collection;
                this.total = response.total;
                this.stopAnimateLoadUsers();
            });
    }

    private animateLoadUsers() {
        this.loading = true;
    }

    private stopAnimateLoadUsers(){
        this.loading = false;
    }

    public loadResources() {
        this.animateLoadUsers();
        this.userService.getUsers(this.quantity, this.page)
            .subscribe(a => {
                this.users = a.collection;
                this.total = a.total;
                this.stopAnimateLoadUsers();
            });
    }

    public remove(user: UserProfile) {
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

                    this.userService.remove(user.userName).subscribe(
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

    public findUser(event: any) {
        if (event.target.value == null || event.target.value === "") {
            this.loadResources();
        }

        this.userSearch.next(event.target.value);
    }

}
