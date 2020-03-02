import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { TranslatorService } from '@core/translator/translator.service';
import { UserService } from '@shared/services/user.service';
import { EventHistoryData } from '@shared/viewModel/event-history-data.model';
import { EventSelector } from '@shared/viewModel/event-selector';
import { ListOf } from '@shared/viewModel/list-of.model';
import { UserProfile } from '@shared/viewModel/userProfile.model';
import { forkJoin, Observable, Subject } from 'rxjs';
import { debounceTime, flatMap, map, switchMap, tap } from 'rxjs/operators';

import { EventsService } from '../events.service';

@Component({
    selector: "app-events",
    templateUrl: "./events.component.html",
    styleUrls: ["./events.component.scss"],
    providers: [UserService]
})
export class EventsComponent implements OnInit {

    public model: EventHistoryData[];
    public selectedType: string;
    public total: number;
    public page: number = 1;
    public quantity: number = 10;
    private eventSearch: Subject<string> = new Subject<string>();

    public eventsToSelect: EventSelector[];
    public aggregatesToSelect: Array<string>;

    public selectedAggregate: string;
    aggregatesTypes: string[];

    constructor(
        private route: ActivatedRoute,
        public translator: TranslatorService,
        private eventService: EventsService) { }

    ngOnInit() {
        this.eventService.listAggregates().subscribe(s => {
            this.eventsToSelect = s;
            this.aggregatesTypes = Array.from(new Set(this.eventsToSelect.map(m => m.aggregateType)));
        });

        this.eventSearch
            .pipe(debounceTime(500))
            .pipe(switchMap(text => this.eventService.searchEvents(text, this.quantity, this.page)))
            .subscribe((response: ListOf<EventHistoryData>) => {
                this.model = response.collection;
                this.total = response.total;
                this.model.forEach(e => e.show = false);
            });
        this.loadResources();
    }

    public loadResources() {
        this.eventSearch.next(this.selectedAggregate == null ? "" : this.selectedAggregate);
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

        let htmlContent = `<pre>${JSON.stringify(JSON.parse(item.details), null, 4)}</pre>`;

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
        // set all others items as show = false
        this.model.forEach(e => {
            e.show = false;
        });
    }

    public getAggregates(type: string) {
        this.aggregatesToSelect = this.eventsToSelect.filter(f => f.aggregateType == type).map(m => m.aggregate);
    }

}
