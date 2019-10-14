import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { User } from "../../shared/models/user.model";
import { environment } from "@env/environment";
import { SetPassword } from "../../shared/view-model/set-password.model";
import { ChangePassword } from "../../shared/view-model/change-password.model";
import { ProfilePictureViewModel } from "../../shared/view-model/file-upload.model";
import { EventHistoryData } from "../../shared/models/event-history-data.model";

@Injectable()
export class AccountManagementService {
    endpoint: string;

    constructor(private http: HttpClient) {
        this.endpoint = environment.ResourceServer + "accounts";
    }

    public update(user: User): Observable<boolean> {
        return this.http.put<boolean>(`${this.endpoint}/update-profile`, user);
    }

    public updatePicture(image: ProfilePictureViewModel): Observable<boolean> {
        return this.http.put<boolean>(`${this.endpoint}/update-profile-picture`, image);
    }

    public addPassword(password: SetPassword): Observable<boolean> {
        return this.http.put<boolean>(`${this.endpoint}/create-password`, password);
    }

    public updatePassword(password: ChangePassword): Observable<boolean> {
        return this.http.put<boolean>(`${this.endpoint}/change-password`, password);
    }

    public deleteAccount(): Observable<void> {
        return this.http.delete<void>(`${this.endpoint}`, {});
    }

    public getUserData(): Observable<User> {
        return this.http.get<User>(`${this.endpoint}`);
    }

    public hasPassword(): Observable<boolean> {
        return this.http.get<boolean>(`${this.endpoint}/has-password`);
    }
    
    public getLogs(): Observable<EventHistoryData> {
        return this.http.get<EventHistoryData>(`${this.endpoint}/logs`);
    }
}
