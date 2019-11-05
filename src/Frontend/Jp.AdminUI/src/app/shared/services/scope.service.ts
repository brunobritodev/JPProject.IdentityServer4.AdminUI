import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';

@Injectable()
export class ScopeService {
   

    endpoint: string;

    constructor(private http: HttpClient) {
        this.endpoint = environment.ResourceServer + "scopes";
    }

    public getScopes(text: string): Observable<string[]> {
        return this.http.get<string[]>(`${this.endpoint}/${text}`);
    }
}
