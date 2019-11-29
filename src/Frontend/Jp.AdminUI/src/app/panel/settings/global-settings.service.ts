import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { GlobalSettings } from '@shared/viewModel/global-settings.model';
import { Observable } from 'rxjs';

@Injectable()
export class GlobalSettingsService {
   
    endpoint: string;


    constructor(private http: HttpClient) {
        this.endpoint = environment.ResourceServer + "global-configuration";
    }


    public getSettings(): Observable<Array<GlobalSettings>> {
        return this.http.get<Array<GlobalSettings>>(`${this.endpoint}`);
    }

    public update(model: Array<GlobalSettings>) {
        return this.http.put<void>(`${this.endpoint}`, model);
    }

}
