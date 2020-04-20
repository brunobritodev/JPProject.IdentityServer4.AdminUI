import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { ApiResource, ApiResourceSecret } from '@shared/viewModel/api-resource.model';
import { Scope } from '@shared/viewModel/scope.model';
import { Operation } from 'fast-json-patch';
import { Observable } from 'rxjs';


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

    public update(resource: string, model: ApiResource): Observable<void> {
        return this.http.put<void>(`${this.endpoint}/${resource}`, model);
    }

    public partialUpdate(resource: string, patch: Operation[]): Observable<void> {
        return this.http.patch<void>(`${this.endpoint}/${resource}`, patch);
    }

    public remove(name: string): any {
        return this.http.delete<void>(`${this.endpoint}/${name}`);
    }

    public getSecrets(resourceName: string): Observable<ApiResourceSecret[]> {
        return this.http.get<ApiResourceSecret[]>(`${this.endpoint}/${resourceName}/secrets`);
    }

    public removeSecret(resourceName: string, type: string, value: string): Observable<void> {
        const params = new HttpParams()
                            .set('type', type)
                            .set('value', encodeURIComponent(value));

        return this.http.delete<void>(`${this.endpoint}/${resourceName}/secrets`, { params });
    }

    public saveSecret(model: ApiResourceSecret): Observable<ApiResourceSecret[]> {
        return this.http.post<ApiResourceSecret[]>(`${this.endpoint}/${model.resourceName}/secrets`, model);
    }

    public getScopes(resourceName: string): Observable<Scope[]> {
        return this.http.get<Scope[]>(`${this.endpoint}/${resourceName}/scopes`);
    }
    public removeScope(resourceName: string, name: string): Observable<void> {
        return this.http.delete<void>(`${this.endpoint}/${resourceName}/scopes/${name}`);
    }

    public saveScope(model: Scope): Observable<Scope[]> {
        return this.http.post<Scope[]>(`${this.endpoint}/${model.resourceName}/scopes`, model);
    }


}
