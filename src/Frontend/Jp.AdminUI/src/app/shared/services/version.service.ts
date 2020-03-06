import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '@env/environment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class VersionService {


    endpoint: string;

    constructor(private http: HttpClient) {
        this.endpoint = environment.ResourceServer + "version";
    }

    public getVersion(): Observable<boolean> {
        return this.http.get(`${this.endpoint}`, { responseType: 'text' })
            .pipe(map(t =>  JSON.parse(t) == "light"));
    }

}
