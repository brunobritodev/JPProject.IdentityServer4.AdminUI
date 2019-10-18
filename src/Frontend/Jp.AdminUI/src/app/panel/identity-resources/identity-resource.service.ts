import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { IdentityResource } from '@shared/viewModel/identity-resource.model';
import { Operation } from 'fast-json-patch';
import { Observable } from 'rxjs';

@Injectable()
export class IdentityResourceService {
    endpoint: string;


    constructor(private http: HttpClient) {
        this.endpoint = environment.ResourceServer + "identity-resources";
    }


    public getIdentityResources(): Observable<IdentityResource[]> {
        return this.http.get<IdentityResource[]>(`${this.endpoint}`);
    }

    public getIdentityResourceDetails(name: string): Observable<IdentityResource> {
        return this.http.get<IdentityResource>(`${this.endpoint}/${name}`);
    }

    public save(model: IdentityResource): Observable<IdentityResource> {
        return this.http.post<IdentityResource>(`${this.endpoint}`, model);
    }

    public update(resource: string, model: IdentityResource): Observable<void> {
        return this.http.put<void>(`${this.endpoint}/${resource}`, model);
    }

    public partialUpdate(resource: string, patch: Operation[]): Observable<void> {
        return this.http.patch<void>(`${this.endpoint}/${resource}`, patch);
    }

    public remove(name: string): any {
        return this.http.delete<void>(`${this.endpoint}/${name}`);
    }


}
