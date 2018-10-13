import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "@env/environment";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { Observable } from "rxjs";

@Injectable()
export class ScopeService {
   

    constructor(private http: HttpClient) {
    }

    public getScopes(text: string): Observable<DefaultResponse<string[]>> {
        let options = {
            params: {
                search: text
            }
        };
        return this.http.get<DefaultResponse<string[]>>(environment.ResourceServer + "scopes/search", options);
    }
}
