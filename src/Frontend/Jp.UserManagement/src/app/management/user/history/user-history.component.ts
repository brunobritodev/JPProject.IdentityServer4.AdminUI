import { Component, OnInit } from '@angular/core';
import { AccountManagementService } from '../account-management.service';
import { Observable } from 'rxjs';
import { EventHistoryData } from '../../../shared/models/event-history-data.model';
import { map } from 'rxjs/operators';
import { TranslatorService } from '@core/translator/translator.service';
@Component({
    templateUrl: 'user-history.component.html',
    providers: [AccountManagementService, TranslatorService],

})
export class UserHistoryComponent implements OnInit {

    public historyData$: Observable<EventHistoryData>;
    public loading: boolean;
    constructor(private accountService: AccountManagementService,public translator: TranslatorService) {
    
    }

    ngOnInit() {
        this.historyData$ = this.accountService.getLogs().pipe(map(a => a.data));
    }

    parse(details: string) {
        return JSON.parse(details);
    }
}
