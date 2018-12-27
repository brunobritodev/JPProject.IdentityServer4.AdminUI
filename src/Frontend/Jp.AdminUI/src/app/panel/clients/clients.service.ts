import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Client, ClientSecret, ClientProperty, NewClient, ClientClaim } from "@shared/viewModel/client.model";

import { environment } from "@env/environment";
import { Observable } from "rxjs";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { ClientList } from "@shared/viewModel/client-list.model";

@Injectable()
export class ClientService {
    

    constructor(private http: HttpClient) {
    }


    public getClients(): Observable<DefaultResponse<ClientList[]>> {
        return this.http.get<DefaultResponse<ClientList[]>>(environment.ResourceServer + "clients/list");
    }

    public getClientDetails(clientId: string): Observable<DefaultResponse<Client>> {
        let options = {
            params: {
                clientId: clientId
            }
        };
        return this.http.get<DefaultResponse<Client>>(environment.ResourceServer + "clients/details", options);
    }

    public save(model: NewClient): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "clients/save", model);
    }

    public update(model: Client): Observable<DefaultResponse<boolean>> {
        return this.http.put<DefaultResponse<boolean>>(environment.ResourceServer + "clients/update", model);
    }

    public remove(clientId: string): any {
        const removeCommand = {
            clientId: clientId
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "clients/remove", removeCommand);
    }

    public getClientSecrets(clientId: string): Observable<DefaultResponse<ClientSecret[]>> {
        let options = {
            params: {
                clientId: clientId
            }
        };
        return this.http.get<DefaultResponse<ClientSecret[]>>(environment.ResourceServer + "clients/secrets", options);
    }
    public removeSecret(client: string, id: number): Observable<DefaultResponse<boolean>> {
        const removeCommand = {
            id: id,
            clientId: client
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "clients/remove-secret", removeCommand);
    }

    public saveSecret(model: ClientSecret): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "clients/save-secret", model);
    }

    public getClientProperties(clientId: string): Observable<DefaultResponse<ClientProperty[]>> {
        let options = {
            params: {
                clientId: clientId
            }
        };
        return this.http.get<DefaultResponse<ClientProperty[]>>(environment.ResourceServer + "clients/properties", options);
    }

    public removeProperty(client: string, id: number): Observable<DefaultResponse<boolean>> {
        const removeCommand = {
            id: id,
            clientId: client
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "clients/remove-property", removeCommand);
    }

    public saveProperty(model: ClientProperty): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "clients/save-property", model);
    }

    public getClientClaims(clientId: string): Observable<DefaultResponse<ClientClaim[]>> {
        let options = {
            params: {
                clientId: clientId
            }
        };
        return this.http.get<DefaultResponse<ClientClaim[]>>(environment.ResourceServer + "clients/claims", options);
    }

    public removeClaim(client: string, id: number): Observable<DefaultResponse<boolean>> {
        const removeCommand = {
            id: id,
            clientId: client
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "clients/remove-claim", removeCommand);
    }

    public saveClaim(model: ClientClaim): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "clients/save-claim", model);
    }

    public copy(clientId: string): Observable<DefaultResponse<boolean>> {
        const command = {
            clientId: clientId
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "clients/copy", command);
    }
    

}
