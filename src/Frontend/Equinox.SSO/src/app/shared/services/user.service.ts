import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { DefaultResponse } from "../view-model/default-response.model";
import { User } from "../models/user.model";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";

@Injectable()
export class UserService {
  


    constructor(private http: HttpClient) {
        // set token if saved in local storage
    }
    public register(register: User): Observable<DefaultResponse<User>> {
        return this.http.post<DefaultResponse<User>>(environment.API_URL + "user/register", register);
    }

    public checkUserName(userName: string): Observable<DefaultResponse<boolean>> {
        const params = {
            username: userName
        };
        return this.http.get<DefaultResponse<boolean>>(environment.API_URL + "user/checkUsername", { params: params });
    }

    public checkEmail(email: string): Observable<DefaultResponse<boolean>> {
        const params = {
            email: email
        };
        return this.http.get<DefaultResponse<boolean>>(environment.API_URL + "user/checkEmail", { params: params });
    }
}