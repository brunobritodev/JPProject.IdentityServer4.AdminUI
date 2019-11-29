import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { ClientList } from '@shared/viewModel/client-list.model';
import { Client, ClientClaim, ClientProperty, ClientSecret, NewClient } from '@shared/viewModel/client.model';
import { ProblemDetails } from '@shared/viewModel/default-response.model';
import { Email } from '@shared/viewModel/email.model';
import { Operation } from 'fast-json-patch';
import { Observable } from 'rxjs';

@Injectable()
export class EmailService {
   
    endpoint: string;


    constructor(private http: HttpClient) {
        this.endpoint = environment.ResourceServer + "emails";
    }


    public getEmailTypes(): Observable<Array<any>> {
        return this.http.get<Array<any>>(`${this.endpoint}/types`);
    }

    public getEmail(type: string) {
        return this.http.get<Email>(`${this.endpoint}/${type}`);
    }
    
    public update(type: string, model: Email) {
        return this.http.put<void>(`${this.endpoint}/${type}`, model);
    }

}
