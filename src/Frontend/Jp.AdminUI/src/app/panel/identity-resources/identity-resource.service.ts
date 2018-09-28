import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";


import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { DefaultResponse } from "../../shared/viewModel/default-response.model";
import { IdentityResource } from "../../shared/viewModel/identity-resource.model";

@Injectable()
export class IdentityResourceService {
    

    constructor(private http: HttpClient) {
    }


    public getIdentityResources(): Observable<DefaultResponse<IdentityResource[]>> {
        return this.http.get<DefaultResponse<IdentityResource[]>>(environment.ResourceServer + "IdentityResource/list");
    }

    public getClientDetails(clientId: string): Observable<DefaultResponse<IdentityResource>> {
        let options = {
            params: {
                clientId: clientId
            }
        };
        return this.http.get<DefaultResponse<IdentityResource>>(environment.ResourceServer + "clients/details", options);
    }

    public save(model: IdentityResource): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "clients/save", model);
    }

    public update(model: IdentityResource): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "clients/update", model);
    }

    public remove(clientId: string): any {
        const removeCommand = {
            clientId: clientId
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "clients/remove", removeCommand);
    }

    public copy(clientId: string): Observable<DefaultResponse<boolean>> {
        const command = {
            clientId: clientId
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "clients/copy", command);
    }
    

}
