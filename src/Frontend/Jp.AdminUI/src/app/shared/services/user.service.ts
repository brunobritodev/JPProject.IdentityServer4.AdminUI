import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "@env/environment";
import { Observable } from "rxjs";
import { DefaultResponse } from "../viewModel/default-response.model";
import { UserProfile, ListOfUsers } from "../viewModel/userProfile.model";
import { UserClaim } from "../viewModel/user-claim.model";
import { UserRole } from "../viewModel/user-role.model";
import { UserLogin } from "../viewModel/user-login.model";
import { ResetPassword } from "../viewModel/reset-password.model";
import { EventHistoryData } from "../viewModel/event-history-data.model";
import { map } from "rxjs/operators";

@Injectable()
export class UserService {
    

    constructor(private http: HttpClient) {
    }

    public getUsers(quantity: number, page: number): Observable<DefaultResponse<ListOfUsers>> {
        return this.http.get<DefaultResponse<ListOfUsers>>(environment.ResourceServer + `UserAdmin/list?q=${quantity}&p=${page}`);
    }
    
    public findUsers(text: string, quantity: number, page: number): Observable<DefaultResponse<ListOfUsers>> {
        return this.http.get<DefaultResponse<ListOfUsers>>(environment.ResourceServer + `UserAdmin/list?q=${quantity}&p=${page}&s=${text}`);
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

        return this.http.put<DefaultResponse<boolean>>(environment.ResourceServer + "UserAdmin/update", updateCommand);
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

    public getUserRoles(userName: string): Observable<DefaultResponse<UserRole[]>> {
        let options = {
            params: {
                userName: userName
            }
        };
        return this.http.get<DefaultResponse<UserRole[]>>(environment.ResourceServer + "UserAdmin/roles", options);
    }

    public removeRole(username: string, role: string): Observable<DefaultResponse<boolean>> {
        const removeCommand = {
            role: role,
            username: username
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "UserAdmin/remove-role", removeCommand);
    }

    public saveRole(model: UserRole): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "UserAdmin/save-role", model);
    }

    public getAvailableRoles(): Observable<DefaultResponse<UserRole[]>> {
        return this.http.get<DefaultResponse<UserRole[]>>(environment.ResourceServer + "UserAdmin/all-roles");
    }

    public getUserLogins(username: string): Observable<DefaultResponse<UserLogin[]>> {
        let options = {
            params: {
                userName: username
            }
        };
        return this.http.get<DefaultResponse<UserLogin[]>>(environment.ResourceServer + "UserAdmin/logins", options);
    }

    public removeLogin(userName: string, loginProvider: string, providerKey: string): any {
        const removeCommand = {
            loginProvider: loginProvider,
            providerKey: providerKey,
            username: userName
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "UserAdmin/remove-login", removeCommand);
    }

    public checkUserName(userName: string): Observable<DefaultResponse<boolean>> {
        const params = {
            username: userName
        };
        return this.http.get<DefaultResponse<boolean>>(environment.ResourceServer + "user/checkUsername", { params: params });
    }

    public checkEmail(email: string): Observable<DefaultResponse<boolean>> {
        const params = {
            email: email
        };
        return this.http.get<DefaultResponse<boolean>>(environment.ResourceServer + "user/checkEmail", { params: params });
    }

    public getUsersFromRole(role: string): Observable<UserProfile[]> {
        let options = {
            params: {
                role: role
            }
        };
        return this.http.get<DefaultResponse<UserProfile[]>>(environment.ResourceServer + "UserAdmin/users-from-role", options).pipe(map(a => a.data));
    }

    public resetPassword(resetPassword: ResetPassword): Observable<DefaultResponse<boolean>> {
        return this.http.put<DefaultResponse<boolean>>(environment.ResourceServer + "UserAdmin/reset-password", resetPassword);
    }

    public showLogs(username: string): Observable<EventHistoryData[]> {
        let options = {
            params: {
                username: username
            }
        };
        return this.http.get<DefaultResponse<EventHistoryData[]>>(environment.ResourceServer + "UserAdmin/show-logs", options).pipe(map(a => a.data));
    }
}
