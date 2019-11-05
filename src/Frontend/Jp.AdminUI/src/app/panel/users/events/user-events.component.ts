import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslatorService } from '@core/translator/translator.service';
import { UserService } from '@shared/services/user.service';
import { EventHistoryData } from '@shared/viewModel/event-history-data.model';
import { ListOf } from '@shared/viewModel/list-of.model';
import { UserProfile } from '@shared/viewModel/userProfile.model';
import { forkJoin, Observable, Subject } from 'rxjs';
import { debounceTime, flatMap, map, switchMap, tap } from 'rxjs/operators';

@Component({
    selector: "app-user-events",
    templateUrl: "./user-events.component.html",
    styleUrls: ["./user-events.component.scss"],
    providers: [UserService]
})
export class UserEventsComponent implements OnInit {

    public model: EventHistoryData[];

    public total: number;
    public page: number = 1;
    public quantity: number = 10;
    private eventSearch: Subject<string> = new Subject<string>();
    public user: UserProfile;

    constructor(
        private route: ActivatedRoute,
        public translator: TranslatorService,
        private userService: UserService) { }

    ngOnInit() {
        this.route.params
            .pipe(
                flatMap(p => this.userService.getDetails(p["username"])),
                tap(user => this.user = user),
                tap(() => this.loadResources())
            )
            .subscribe();

        this.eventSearch
            .pipe(debounceTime(500))
            .pipe(switchMap(text => this.userService.searchEvents(this.user.userName, text, this.quantity, this.page)))
            .subscribe((response: ListOf<EventHistoryData>) => {
                this.model = response.collection;
                this.total = response.total;
            });
    }

    public loadResources() {
        this.userService.showEvents(this.user.userName, this.quantity, this.page)
            .subscribe((response: ListOf<EventHistoryData>) => {
                this.model = response.collection;
                this.total = response.total;
            });
    }

    public findEvent(event: any) {
        if (event.target.value == null || event.target.value === "") {
            this.loadResources();
        }

        this.eventSearch.next(event.target.value);
    }

    parse(details: string) {
        return JSON.parse(details);
    }
}
