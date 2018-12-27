import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "@env/environment";
import { Observable } from "rxjs";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { ApiResource, ApiResourceSecret } from "@shared/viewModel/api-resource.model";
import { Scope } from "@shared/viewModel/scope.model";


@Injectable()
export class ApiResourceService {

    constructor(private http: HttpClient) {
    }


    public getApiResources(): Observable<DefaultResponse<ApiResource[]>> {
        return this.http.get<DefaultResponse<ApiResource[]>>(environment.ResourceServer + "ApiResource/list");
    }

    public getApiResourceDetails(name: string): Observable<DefaultResponse<ApiResource>> {
        let options = {
            params: {
                name: name
            }
        };
        return this.http.get<DefaultResponse<ApiResource>>(environment.ResourceServer + "ApiResource/details", options);
    }

    public save(model: ApiResource): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "ApiResource/save", model);
    }

    public update(model: ApiResource): Observable<DefaultResponse<boolean>> {
        return this.http.put<DefaultResponse<boolean>>(environment.ResourceServer + "ApiResource/update", model);
    }

    public remove(name: string): any {
        const removeCommand = {
            name: name
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "ApiResource/remove", { params: removeCommand });
    }


    public getSecrets(resourceName: string): Observable<DefaultResponse<ApiResourceSecret[]>> {
        let options = {
            params: {
                name: resourceName
            }
        };
        return this.http.get<DefaultResponse<ApiResourceSecret[]>>(environment.ResourceServer + "ApiResource/secrets", options);
    }

    public removeSecret(resourceName: string, id: number): Observable<DefaultResponse<boolean>> {
        
        const removeCommand = {
            id: id,
            resourceName: resourceName
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "ApiResource/remove-secret", removeCommand);
    }

    public saveSecret(model: ApiResourceSecret): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "ApiResource/save-secret", model);
    }

    public getScopes(resourceName: string): Observable<DefaultResponse<Scope[]>> {
        let options = {
            params: {
                name: resourceName
            }
        };
        return this.http.get<DefaultResponse<Scope[]>>(environment.ResourceServer + "ApiResource/scopes", options);
    }
    public removeScope(resourceName: string, id: number): Observable<DefaultResponse<boolean>> {
        const removeCommand = {
            id: id,
            resourceName: resourceName
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "ApiResource/remove-scope", removeCommand);
    }

    public saveScope(model: Scope): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "ApiResource/save-scope", model);
    }


}
