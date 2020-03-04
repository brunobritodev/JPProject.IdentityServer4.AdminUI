import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SettingsService } from '@core/settings/settings.service';
import { environment } from '@env/environment';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { ListOf } from '@shared/viewModel/list-of.model';
import { ListOfPersistedGrant, PersistedGrant } from '@shared/viewModel/persisted-grants.model';
import { Observable } from 'rxjs';

@Injectable()
export class PersistedGrantsService {

    endpoint: string;

    constructor(private http: HttpClient, private settings: SettingsService) {
        this.endpoint = environment.ResourceServer + "persisted-grants";
    }

    public getPersistedGrants(quantity: number, page: number): Observable<ListOf<PersistedGrant>> {
        return this.http.get<ListOf<PersistedGrant>>(`${this.endpoint}?limit=${quantity}&offset=${(page - 1) * quantity}`);
    }

    public remove(key: string): Observable<void> {
        return this.http.delete<void>(`${this.endpoint}/${btoa(key)}`);
    }
}
