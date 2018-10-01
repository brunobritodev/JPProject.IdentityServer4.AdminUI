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

    public getPersistedGrantsDetails(name: string): Observable<DefaultResponse<PersistedGrant>> {
        let options = {
            params: {
                name: name
            }
        };
        return this.http.get<DefaultResponse<PersistedGrant>>(environment.ResourceServer + "PersistedGrants/details", options);
    }

    public save(model: PersistedGrant): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "PersistedGrants/save", model);
    }

    public update(model: PersistedGrant): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "PersistedGrants/update", model);
    }

    public remove(name: string): any {
        const removeCommand = {
            name: name
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "PersistedGrant/remove", removeCommand);
    }


}
