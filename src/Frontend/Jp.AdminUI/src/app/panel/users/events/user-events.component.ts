import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslatorService } from '@core/translator/translator.service';
import { UserService } from '@shared/services/user.service';
import { EventHistoryData } from '@shared/viewModel/event-history-data.model';
import { ListOf } from '@shared/viewModel/list-of.model';
import { UserProfile } from '@shared/viewModel/userProfile.model';
import { forkJoin, Observable, Subject } from 'rxjs';
import { catchError, debounceTime, flatMap, map, switchMap, tap } from 'rxjs/operators';

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
        private router: Router,
        public translator: TranslatorService,
        private userService: UserService) { }

    ngOnInit() {
        this.route.params
            .pipe(
                flatMap(p => this.userService.getDetails(p["username"])),
                tap(user => this.user = user),
            )
            .subscribe(s => this.loadResources(),
                err => {
                    this.router.navigate(['/users']);
                });

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
                this.setEveryoneToNotShow();
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

    public addRow(index: number, tableName: string) {

        var table = document.getElementById(tableName) as HTMLTableElement;

        // Hide everyone before. In case users click in another item from list
        table.querySelectorAll("[temp='true']").forEach((i: HTMLTableRowElement) => i.remove());
        var item = this.model[index];

        if (item.show) {
            this.setEveryoneToNotShow();
            return;
        }

        let htmlContent = `<pre class="pre-scrollable-width">${JSON.stringify(JSON.parse(item.details), null, 4)}</pre>`;

        // Create an empty <tr> element and add it to the 1st position of the table:
        var row = table.insertRow(index + 2);
        row.setAttribute("temp", "true")

        // Insert new cells (<td> elements) at the 1st and 2nd position of the "new" <tr> element:
        var cell = row.insertCell(0);
        // Add some text to the new cells:
        cell.innerHTML = htmlContent;
        cell.colSpan = 7;

        this.setEveryoneToNotShow();
        item.show = true;
    }

    private setEveryoneToNotShow() {
        if (this.model == null)
            return;
        // set all others items as show = false
        this.model.forEach(e => {
            e.show = false;
        });
    }
}
