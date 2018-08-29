import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { User } from "../../../shared/models/user.model";
import { environment } from "../../../../environments/environment";
import { DefaultResponse } from "../../../shared/view-model/default-response.model";

@Injectable()
export class ProfileService {
    
    
    
    constructor(private http: HttpClient) {
        // set token if saved in local storage
    }
    

    public getUserData(): Observable<DefaultResponse<User>> {
        return this.http.get<DefaultResponse<User>>(environment.ResourceServer + "management/user-info");
    }

}