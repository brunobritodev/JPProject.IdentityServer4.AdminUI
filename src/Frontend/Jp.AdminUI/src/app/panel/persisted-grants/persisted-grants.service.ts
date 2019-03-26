import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "@env/environment";
import { Observable } from "rxjs";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { ListOfPersistedGrant } from "@shared/viewModel/persisted-grants.model";

@Injectable()
export class PersistedGrantsService {

    constructor(private http: HttpClient) {
    }


    public getPersistedGrants(quantity: number, page: number): Observable<DefaultResponse<ListOfPersistedGrant>> {
        return this.http.get<DefaultResponse<ListOfPersistedGrant>>(environment.ResourceServer + `PersistedGrants/list?q=${quantity}&p=${page}`);
    }

    public remove(key: string): any {
        const removeCommand = {
            key: key
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "PersistedGrants/remove", removeCommand);
    }


}
