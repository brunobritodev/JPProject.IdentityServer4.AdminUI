import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { environment } from "../../../environments/environment";
import { Observable } from "rxjs";
import { DefaultResponse } from "../../shared/viewModel/default-response.model";
import { Role } from "../viewModel/role.model";

@Injectable()
export class RoleService {

    constructor(private http: HttpClient) {
    }

    public getAvailableRoles(): Observable<DefaultResponse<Role[]>>  {
        return this.http.get<DefaultResponse<Role[]>>(environment.ResourceServer + "Roles/all-roles");
    }
}
