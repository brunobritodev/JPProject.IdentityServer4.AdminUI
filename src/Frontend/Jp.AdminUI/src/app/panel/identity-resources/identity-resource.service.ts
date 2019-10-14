import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "@env/environment";
import { Observable } from "rxjs";
import { IdentityResource } from "@shared/viewModel/identity-resource.model";

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

    public update(model: IdentityResource): Observable<void> {
        return this.http.put<void>(`${this.endpoint}/${name}`, model);
    }

    public remove(name: string): any {
        return this.http.delete<void>(`${this.endpoint}/${name}`);
    }


}
