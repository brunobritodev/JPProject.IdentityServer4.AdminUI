import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { DefaultResponse } from "../view-model/default-response.model";
import { User } from "../models/user.model";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { LoginInfo } from "../models/login-info.model";

@Injectable()
export class AccountService {

    constructor(private http: HttpClient) {
        // set token if saved in local storage
    }
    public getLoginInfo(): Observable<DefaultResponse<LoginInfo>> {
        return this.http.get<DefaultResponse<LoginInfo>>(environment.API_URL + "account/login-info");
    }

}