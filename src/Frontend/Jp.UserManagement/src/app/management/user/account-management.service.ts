import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { User } from "../../shared/models/user.model";
import { environment } from "@env/environment";
import { DefaultResponse } from "../../shared/view-model/default-response.model";
import { SetPassword } from "../../shared/view-model/set-password.model";
import { ChangePassword } from "../../shared/view-model/change-password.model";
import { ProfilePictureViewModel } from "../../shared/view-model/file-upload.model";
import { EventHistoryData } from "../../shared/models/event-history-data.model";

@Injectable()
export class AccountManagementService {

    constructor(private http: HttpClient) {
        // set token if saved in local storage
    }

    public update(user: User): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "management/update-profile", user);
    }

    public updatePicture(image: ProfilePictureViewModel): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "management/update-profile-picture", image);
    }

    public addPassword(password: SetPassword): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "management/create-password", password);
    }

    public updatePassword(password: ChangePassword): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "management/change-password", password);
    }

    public deleteAccount(): Observable<DefaultResponse<boolean>> {
        return this.http.post<DefaultResponse<boolean>>(environment.ResourceServer + "management/remove-account", {});
    }

    public getUserData(): Observable<DefaultResponse<User>> {
        return this.http.get<DefaultResponse<User>>(environment.ResourceServer + "management/user-data");
    }

    public hasPassword(): Observable<DefaultResponse<boolean>> {
        return this.http.get<DefaultResponse<boolean>>(environment.ResourceServer + "management/has-password");
    }
    
    public getLogs(): Observable<DefaultResponse<EventHistoryData>> {
        return this.http.get<DefaultResponse<EventHistoryData>>(environment.ResourceServer + "management/logs");
    }

}