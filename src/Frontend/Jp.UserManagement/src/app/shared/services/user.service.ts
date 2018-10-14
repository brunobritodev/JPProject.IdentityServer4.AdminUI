import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { DefaultResponse } from "../view-model/default-response.model";
import { User } from "../models/user.model";
import { Observable } from "rxjs";
import { environment } from "@env/environment";
import { ForgotPassword } from "../view-model/forgot-password.model";
import { ResetPassword } from "../view-model/reset-password.model";
import { ConfirmEmail } from "../view-model/confirm-email.model";

@Injectable()
export class UserService {
    
    constructor(private http: HttpClient) {
        // set token if saved in local storage
    }
    public register(register: User): Observable<DefaultResponse<User>> {
        if (register.provider != null)
            return this.http.post<DefaultResponse<User>>(environment.ResourceServer + "user/register-provider", register);
        else
            return this.http.post<DefaultResponse<User>>(environment.ResourceServer + "user/register", register);
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

    public recoverPassword(emailOrPassword: ForgotPassword): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "user/forgot-password", emailOrPassword);
    }

    public resetPassword(model: ResetPassword): any {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "user/reset-password", model);
    }

    public confirmEmail(model: ConfirmEmail): any {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "user/confirm-email", model);
    }
}