import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';

import { User } from '../models/user.model';
import { ConfirmEmail } from '../view-model/confirm-email.model';
import { DefaultResponse } from '../view-model/default-response.model';
import { ForgotPassword } from '../view-model/forgot-password.model';
import { ResetPassword } from '../view-model/reset-password.model';

@Injectable()
export class UserService {

    endpoint: string;
    endpointUser: string;

    constructor(private http: HttpClient) {
        this.endpoint = environment.ResourceServer + "sign-up";
        this.endpointUser = environment.ResourceServer + "user";
    }
    public register(register: User): Observable<User> {
        return this.http.post<User>(`${this.endpoint}` , register);
    }

    public checkUserName(userName: string): Observable<boolean> {
        return this.http.get<boolean>(`${this.endpoint}/check-username/${userName}`);
    }

    public checkEmail(email: string): Observable<boolean> {
        return this.http.get<boolean>(`${this.endpoint}/check-email/${email}`);
    }

    public recoverPassword(emailOrPassword: ForgotPassword): Observable<boolean> {
        return this.http.post<boolean>(`${this.endpointUser}/${emailOrPassword}/password/forget`, emailOrPassword);
    }

    public resetPassword(username: string, model: ResetPassword): any {
        return this.http.post<boolean>(`${this.endpointUser}/${username}/password/reset`, model);
    }

    public confirmEmail(username: string, model: ConfirmEmail): any {
        return this.http.post<boolean>(`${this.endpointUser}/${username}/confirm-email`, model);
    }
}