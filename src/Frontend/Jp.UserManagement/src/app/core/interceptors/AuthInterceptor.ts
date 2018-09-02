import { Injectable, Inject } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { map, catchError } from 'rxjs/operators';
import { of } from 'rxjs/observable/of';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment.prod';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    private baseUrl: string;

    constructor(
        private authStorage: OAuthService,
        private router: Router) {
        this.baseUrl = environment.ResourceServer;
    }

    public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const url = req.url.toLowerCase();

        if (this.shouldIgnore(url))
            return next.handle(req);

        if (url.startsWith(this.baseUrl.toLowerCase())) {

            const token = this.authStorage.getAccessToken();
            const header = 'Bearer ' + token;

            const headers = req.headers
                .set('Authorization', header);

            req = req.clone({ headers });
        }

        return next.handle(req);
    }

    private shouldIgnore(url: string): boolean {
        if (url.startsWith(`${this.baseUrl.toLowerCase()}/user/`))
            return true;
    }

}