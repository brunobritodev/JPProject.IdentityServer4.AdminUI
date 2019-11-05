import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { UserProfile } from '@shared/viewModel/userProfile.model';
import { Observable } from 'rxjs';

import { Role } from '../viewModel/role.model';

@Injectable()
export class RoleService {

    endpoint: string;

    constructor(private http: HttpClient) {
        this.endpoint = environment.ResourceServer + "roles";
    }


    public getAvailableRoles(): Observable<Role[]> {
        return this.http.get<Role[]>(`${this.endpoint}`);
    }

    public remove(name: string): Observable<void> {
        return this.http.delete<void>(`${this.endpoint}/${name}`);
    }

    public getRoleDetails(name: string): Observable<Role> {
        return this.http.get<Role>(`${this.endpoint}/${name}`);
    }

    public update(name: string, model: Role): Observable<void> {
        return this.http.put<void>(`${this.endpoint}/${name}`, model);
    }

    public removeUserFromRole(user: string, role: string): Observable<void> {
        return this.http.delete<void>(`${this.endpoint}/${role}/${user}`);
    }

    public save(model: Role): Observable<Role> {
        return this.http.post<Role>(`${this.endpoint}`, model);
    }

    public getUsersFromRole(role: string): Observable<UserProfile[]> {
        return this.http.get<UserProfile[]>(`${this.endpoint}/${role}/users`);
    }

}
