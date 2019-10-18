import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { ClientList } from '@shared/viewModel/client-list.model';
import { Client, ClientClaim, ClientProperty, ClientSecret, NewClient } from '@shared/viewModel/client.model';
import { DefaultResponse } from '@shared/viewModel/default-response.model';
import { Observable } from 'rxjs';

@Injectable()
export class ClientService {
    endpoint: string;
    

    constructor(private http: HttpClient) {
        this.endpoint = environment.ResourceServer + "clients";
    }


    public getClients(): Observable<ClientList[]> {
        return this.http.get<ClientList[]>(this.endpoint);
    }

    public getClientDetails(clientId: string): Observable<Client> {
        return this.http.get<Client>(`${this.endpoint}/${clientId}`);
    }

    public save(model: NewClient): Observable<Client> {
        return this.http.post<Client>(this.endpoint, model);
    }

    public update(model: Client): Observable<void> {
        return this.http.put<void>(`${this.endpoint}/${model.oldClientId}`, model);
    }

    public copy(clientId: string): Observable<Client> {
        const command = { };
        return this.http.post<Client>(`${this.endpoint}/${clientId}/copy`, command);
    }

    public remove(clientId: string) {
        return this.http.delete(`${this.endpoint}/${clientId}`);
    }

    public getClientSecrets(clientId: string): Observable<ClientSecret[]> {
        return this.http.get<ClientSecret[]>(`${this.endpoint}/${clientId}/secrets`);
    }

    public removeSecret(client: string, id: number): Observable<void> {
        return this.http.delete<void>(`${this.endpoint}/${client}/secrets/${id}`);
    }

    public saveSecret(model: ClientSecret): Observable<ClientSecret[]> {
        return this.http.post<ClientSecret[]>(`${this.endpoint}/${model.clientId}/secrets` , model);
    }

    public getClientProperties(clientId: string): Observable<ClientProperty[]> {
        return this.http.get<ClientProperty[]>(`${this.endpoint}/${clientId}/properties`);
    }

    public removeProperty(client: string, id: number): Observable<void> {
        return this.http.delete<void>(`${this.endpoint}/${client}/properties/${id}`);
    }

    public saveProperty(model: ClientProperty): Observable<ClientProperty[]> {
        return this.http.post<ClientProperty[]>(`${this.endpoint}/${model.clientId}/properties`, model);
    }

    public getClientClaims(clientId: string): Observable<ClientClaim[]> {
        return this.http.get<ClientClaim[]>(`${this.endpoint}/${clientId}/claims`);
    }

    public removeClaim(client: string, id: number): Observable<void> {
        return this.http.delete<void>(`${this.endpoint}/${client}/claims/${id}`);
    }

    public saveClaim(model: ClientClaim): Observable<ClientClaim[]> {
        return this.http.post<ClientClaim[]>(`${this.endpoint}/${model.clientId}/claims`, model);
    }
}
