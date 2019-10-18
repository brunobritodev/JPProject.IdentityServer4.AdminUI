import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { DefaultResponse } from '@shared/viewModel/default-response.model';
import { ListOfPersistedGrant } from '@shared/viewModel/persisted-grants.model';
import { Observable } from 'rxjs';

@Injectable()
export class PersistedGrantsService {

    endpoint: string;

    constructor(private http: HttpClient) {
        this.endpoint = environment.ResourceServer + "persisted-grants";
    }

    public getPersistedGrants(quantity: number, page: number): Observable<ListOfPersistedGrant> {
        return this.http.get<ListOfPersistedGrant>(`${this.endpoint}?limit=${quantity}&offset=${page}`);
    }

    public remove(key: string): any {
        return this.http.delete<void>(`${this.endpoint}/${key}`);
    }


}
