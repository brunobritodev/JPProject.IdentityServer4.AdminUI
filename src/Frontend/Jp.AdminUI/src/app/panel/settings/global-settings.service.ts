import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { GlobalSettings } from '@shared/viewModel/global-settings.model';
import { LdapConnectionResult } from '@shared/viewModel/ldap.model';
import { Observable } from 'rxjs';

@Injectable()
export class GlobalSettingsService {


    endpoint: string;


    constructor(private http: HttpClient) {
        this.endpoint = environment.ResourceServer + "global-configuration";
    }


    public getSettings(): Observable<Array<GlobalSettings>> {
        return this.http.get<Array<GlobalSettings>>(`${this.endpoint}`);
    }

    public update(model: Array<GlobalSettings>) {
        return this.http.put<void>(`${this.endpoint}`, model);
    }

    public testLdap(attributes: string, authType: string, distinguishedName: string, domainName: string, searchScope: string, address: string, portNumber: number, username: string, password: string) {
        let params = new HttpParams()
            .set('distinguishedName', distinguishedName)
            .set('domainName', domainName)
            .set('searchScope', searchScope)
            .set('address', address)
            .set('portNumber', portNumber.toString());

        if (authType != null && authType != "")
            params = params.set('authType', authType);
        if (attributes != null && attributes != "")
            params = params.set('attributes', attributes);
        if (username != null && username != "")
            params = params.set('username', username);
        if (password != null && password != "")
            params = params.set('password', password);

        return this.http.get<LdapConnectionResult>(`${this.endpoint}/ldap-test`, { params });
    }

}
