import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { DefaultResponse } from "../../shared/viewModel/default-response.model";
import { UserProfile } from "../../shared/viewModel/userProfile.model";
import { UserClaim } from "../../shared/viewModel/user-claim.model";

@Injectable()
export class UserService {



    constructor(private http: HttpClient) {
    }


    public getUsers(): Observable<DefaultResponse<UserProfile[]>> {
        return this.http.get<DefaultResponse<UserProfile[]>>(environment.ResourceServer + "UserAdmin/list");
    }

    public getDetails(username: string): Observable<DefaultResponse<UserProfile>> {
        let options = {
            params: {
                username: username
            }
        };
        return this.http.get<DefaultResponse<UserProfile>>(environment.ResourceServer + "UserAdmin/details", options);
    }

    public update(updateCommand: UserProfile): Observable<DefaultResponse<boolean>> {

        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "UserAdmin/update", updateCommand);
    }
    public save(model: UserProfile): Observable<DefaultResponse<boolean>> {

        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "User/register", model);
    }

    public remove(id: string): Observable<DefaultResponse<boolean>> {
        const removeCommand = {
            id: id
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "UserAdmin/remove-account", removeCommand);
    }

    public getUserClaims(userName: string): Observable<DefaultResponse<UserClaim[]>> {
        let options = {
            params: {
                userName: userName
            }
        };
        return this.http.get<DefaultResponse<UserClaim[]>>(environment.ResourceServer + "UserAdmin/claims", options);
    }
    
    public removeClaim(username: string, type: string): Observable<DefaultResponse<boolean>> {
        const removeCommand = {
            type: type,
            username: username
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "UserAdmin/remove-claim", removeCommand);
    }

    public saveClaim(model: UserClaim): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "UserAdmin/save-claim", model);
    }
}
