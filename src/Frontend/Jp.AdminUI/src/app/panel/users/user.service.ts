import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { DefaultResponse } from "../../shared/viewModel/default-response.model";
import { UserProfile } from "../../shared/viewModel/userProfile.model";

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

    public remove(key: string): any {
        const removeCommand = {
            key: key
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "UserAdmin/remove", removeCommand);
    }

    public update(updateCommand: UserProfile): Observable<DefaultResponse<boolean>> {

        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "UserAdmin/update", updateCommand);
    }
    public save(model: UserProfile): Observable<DefaultResponse<boolean>> {

        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "UserAdmin/save", model);
    }
}
