import { Component, OnInit } from '@angular/core';
import { AccountManagementService } from '../account-management.service';
import { Observable } from 'rxjs';
import { EventHistoryData } from '../../../shared/models/event-history-data.model';
import { map } from 'rxjs/operators';

@Component({
    templateUrl: 'user-history.component.html',
    providers: [AccountManagementService],

})
export class UserHistoryComponent implements OnInit {

    public historyData$: Observable<EventHistoryData>;

    constructor(private accountService: AccountManagementService) {

    }

    ngOnInit() {
        this.historyData$ = this.accountService.getLogs().pipe(map(a => a.data));
    }

    parse(details: string) {
        return JSON.parse(details);
    }
}
