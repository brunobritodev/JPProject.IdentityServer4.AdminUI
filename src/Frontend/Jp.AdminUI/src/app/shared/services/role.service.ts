import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "@env/environment";
import { Observable } from "rxjs";
import { DefaultResponse } from "@shared/viewModel/default-response.model";
import { Role } from "../viewModel/role.model";

@Injectable()
export class RoleService {

    constructor(private http: HttpClient) {
    }

    public getAvailableRoles(): Observable<DefaultResponse<Role[]>> {
        return this.http.get<DefaultResponse<Role[]>>(environment.ResourceServer + "Roles/all-roles");
    }

    public remove(name: string): Observable<DefaultResponse<boolean>> {
        const removeCommand = {
            name: name
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "Roles/remove", removeCommand);
    }

    public getRoleDetails(name: string): Observable<DefaultResponse<Role>> {
        let options = {
            params: {
                name: name
            }
        };
        return this.http.get<DefaultResponse<Role>>(environment.ResourceServer + "Roles/details", options);
    }

    public update(model: Role): Observable<DefaultResponse<boolean>> {
        return this.http.put<DefaultResponse<boolean>>(environment.ResourceServer + "Roles/update", model);
    }

    public removeUserFromRole(user: string, role: string): Observable<DefaultResponse<boolean>> {
        const removeCommand = {
            username: user,
            role: role
        };
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "Roles/remove-user", removeCommand);
    }
    
    public save(model: Role): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "Roles/save", model);
    }

}
