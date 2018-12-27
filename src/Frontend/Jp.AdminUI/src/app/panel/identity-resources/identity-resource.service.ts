import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";


import { environment } from "@env/environment";
import { Observable } from "rxjs";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { IdentityResource } from "@shared/viewModel/identity-resource.model";

@Injectable()
export class IdentityResourceService {
    

    constructor(private http: HttpClient) {
    }


    public getIdentityResources(): Observable<DefaultResponse<IdentityResource[]>> {
        return this.http.get<DefaultResponse<IdentityResource[]>>(environment.ResourceServer + "IdentityResource/list");
    }

    public getIdentityResourceDetails(name: string): Observable<DefaultResponse<IdentityResource>> {
        let options = {
            params: {
                name: name
            }
        };
        return this.http.get<DefaultResponse<IdentityResource>>(environment.ResourceServer + "IdentityResource/details", options);
    }

    public save(model: IdentityResource): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "IdentityResource/save", model);
    }

    public update(model: IdentityResource): Observable<DefaultResponse<boolean>> {
        return this.http.put<DefaultResponse<boolean>>(environment.ResourceServer + "IdentityResource/update", model);
    }

    public remove(name: string): any {
        const removeCommand = {
            name: name
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "IdentityResource/remove", removeCommand);
    }


}
