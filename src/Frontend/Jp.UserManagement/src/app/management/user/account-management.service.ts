import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Operation } from 'fast-json-patch';
import { Observable } from 'rxjs';

import { EventHistoryData } from '../../shared/models/event-history-data.model';
import { User } from '../../shared/models/user.model';
import { ChangePassword } from '../../shared/view-model/change-password.model';
import { ProfilePictureViewModel } from '../../shared/view-model/file-upload.model';
import { SetPassword } from '../../shared/view-model/set-password.model';

@Injectable()
export class AccountManagementService {
    endpoint: string;

    constructor(private http: HttpClient) {
        this.endpoint = environment.ResourceServer + "accounts";
    }

    public update(user: User): Observable<boolean> {
        return this.http.put<boolean>(`${this.endpoint}/profile`, user);
    }

    public partialUpdate(user: Operation[]): Observable<boolean> {
        return this.http.patch<boolean>(`${this.endpoint}/profile`, user);
    }

    public updatePicture(image: ProfilePictureViewModel): Observable<boolean> {
        return this.http.put<boolean>(`${this.endpoint}/profile/picture`, image);
    }

    public addPassword(password: SetPassword): Observable<boolean> {
        return this.http.post<boolean>(`${this.endpoint}/password`, password);
    }

    public updatePassword(password: ChangePassword): Observable<boolean> {
        return this.http.put<boolean>(`${this.endpoint}/password`, password);
    }

    public deleteAccount(): Observable<void> {
        return this.http.delete<void>(`${this.endpoint}`);
    }

    public getUserData(): Observable<User> {
        return this.http.get<User>(`${this.endpoint}/profile`);
    }

    public hasPassword(): Observable<boolean> {
        return this.http.get<boolean>(`${this.endpoint}/password`);
    }
    
    public getLogs(): Observable<EventHistoryData> {
        return this.http.get<EventHistoryData>(`${this.endpoint}/logs`);
    }
}
