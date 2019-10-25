import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';

@Injectable()
export class VersionService {

    endpoint: string;

    constructor(private http: HttpClient) {
        this.endpoint = environment.ResourceServer + "version";
    }

    public getVersion(): Observable<string> {
        return this.http.get(`${this.endpoint}`, { responseType: 'text' });
    }
}
