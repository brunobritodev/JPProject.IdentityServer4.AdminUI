import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { DefaultResponse } from "../../shared/viewModel/default-response.model";
import { PersistedGrant } from "../../shared/viewModel/persisted-grants.model";

@Injectable()
export class PersistedGrantsService {

    constructor(private http: HttpClient) {
    }


    public getPersistedGrants(): Observable<DefaultResponse<PersistedGrant[]>> {
        return this.http.get<DefaultResponse<PersistedGrant[]>>(environment.ResourceServer + "PersistedGrants/list");
    }

    public remove(key: string): any {
        const removeCommand = {
            key: key
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "PersistedGrants/remove", removeCommand);
    }


}
