import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SettingsService } from '@core/settings/settings.service';
import { environment } from '@env/environment';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { EventHistoryData } from '@shared/viewModel/event-history-data.model';
import { EventSelector } from '@shared/viewModel/event-selector';
import { ListOf } from '@shared/viewModel/list-of.model';
import { ListOfPersistedGrant, PersistedGrant } from '@shared/viewModel/persisted-grants.model';
import { Observable } from 'rxjs';

@Injectable()
export class EventsService {

    endpoint: string;

    constructor(private http: HttpClient, private settings: SettingsService) {
        this.endpoint = environment.ResourceServer + "events";
    }

    public searchEvents(aggregate: string, quantity: number, page: number): Observable<ListOf<EventHistoryData>> {
        let params = new HttpParams()
            .set('limit', quantity.toString())
            .set('offset', ((page - 1) * quantity).toString());
        if (aggregate && aggregate != "") {
            params = params.set('aggregate', aggregate);
        }

        return this.http.get<ListOf<EventHistoryData>>(`${this.endpoint}`, { params });
    }

    public listAggregates() {
        return this.http.get<EventSelector[]>(`${this.endpoint}/aggregates`);
    }

}
