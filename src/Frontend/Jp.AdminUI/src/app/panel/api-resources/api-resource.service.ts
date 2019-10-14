import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "@env/environment";
import { Observable } from "rxjs";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { ApiResource, ApiResourceSecret } from "@shared/viewModel/api-resource.model";
import { Scope } from "@shared/viewModel/scope.model";


@Injectable()
export class ApiResourceService {
    endpoint: string;

    constructor(private http: HttpClient) {
        this.endpoint = environment.ResourceServer + "api-resources";
    }


    public getApiResources(): Observable<ApiResource[]> {
        return this.http.get<ApiResource[]>(`${this.endpoint}`);
    }

    public getApiResourceDetails(name: string): Observable<ApiResource> {
        return this.http.get<ApiResource>(`${this.endpoint}/${name}`);
    }

    public save(model: ApiResource): Observable<ApiResource> {
        return this.http.post<ApiResource>(`${this.endpoint}`, model);
    }

    public update(model: ApiResource): Observable<void> {
        return this.http.put<void>(`${this.endpoint}/${model.oldApiResourceId}`, model);
    }

    public remove(name: string): any {
        return this.http.delete<void>(`${this.endpoint}/${name}`);
    }

    public getSecrets(resourceName: string): Observable<ApiResourceSecret[]> {
        return this.http.get<ApiResourceSecret[]>(`${this.endpoint}/${resourceName}/secrets`);
    }

    public removeSecret(resourceName: string, id: number): Observable<void> {
        return this.http.delete<void>(`${this.endpoint}/${resourceName}/secrets/${id}`);
    }

    public saveSecret(model: ApiResourceSecret): Observable<ApiResourceSecret[]> {
        return this.http.post<ApiResourceSecret[]>(`${this.endpoint}/${model.resourceName}/secrets`, model);
    }

    public getScopes(resourceName: string): Observable<Scope[]> {
        return this.http.get<Scope[]>(`${this.endpoint}/${resourceName}/scopes`);
    }
    public removeScope(resourceName: string, id: number): Observable<void> {
        return this.http.delete<void>(`${this.endpoint}/${resourceName}/scopes/${id}`);
    }

    public saveScope(model: Scope): Observable<Scope[]> {
        return this.http.post<Scope[]>(`${this.endpoint}/${model.resourceName}/scopes`, model);
    }


}
