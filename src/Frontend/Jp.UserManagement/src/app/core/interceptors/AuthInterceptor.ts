
import {throwError as observableThrowError,  Observable ,  BehaviorSubject, of, from } from 'rxjs';
import { Injectable } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '@env/environment';
import { switchMap, catchError, take, tap, filter } from 'rxjs/operators';
import { SettingsService } from '../settings/settings.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    private baseUrl: string;
    isRefreshingToken = false;
    tokenSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(null);

    constructor(
        private authStorage: OAuthService,
        public settingsService: SettingsService) {
        this.baseUrl = environment.ResourceServer;
    }

    public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const url = req.url.toLowerCase();

        if (this.shouldIgnore(url))
            return next.handle(req);

        if (url.startsWith(this.baseUrl.toLowerCase())) {

            return next.handle(this.addToken(req, this.authStorage.getAccessToken())).pipe(tap((event: HttpEvent<any>) => { }, (error: any) => {
                if (error instanceof HttpErrorResponse) {
                    switch ((<HttpErrorResponse>error).status) {
                        case 400:
                            return this.handle400Error(error);
                        case 401:
                            {
                                this.settingsService.login();
                                return observableThrowError(error);
                            }
                    }
                } else {
                    return observableThrowError(error);
                }
            }));
        }

        return next.handle(req);
    }

    private handle400Error(error) {
        if (error && error.status === 400 && error.error && error.error.error === 'invalid_grant') {
            // If we get a 400 and the error message is 'invalid_grant', the token is no longer valid so logout.
            this.authStorage.logOut();
            this.settingsService.login();
        }

        return observableThrowError(error);
    }

    // In case of silentRefresh()
    private handle401Error(req: HttpRequest<any>, next: HttpHandler) {
        if (!this.isRefreshingToken) {
            this.isRefreshingToken = true;

            // Reset here so that the following requests wait until the token
            // comes back from the refreshToken call.
            this.tokenSubject.next(null);

            from(this.authStorage.silentRefresh())
                .pipe(switchMap(a => {
                    this.tokenSubject.next(true);
                    return next.handle(this.addToken(req, this.authStorage.getAccessToken()));
                }))
                .pipe(tap((event: HttpEvent<any>) => { }, (err: any) => {
                    if (err instanceof HttpErrorResponse) {
                        // do error handling here
                        this.settingsService.login();
                    }
                }));
        } else {
            return this.tokenSubject
                .pipe(filter(token => token != null))
                .pipe(take(1))
                .pipe(switchMap(token => {
                    return next.handle(this.addToken(req, this.authStorage.getAccessToken()));
                }));
        }
    }

    /*
        This method is only here so the example works.
        Do not include in your code, just use 'req' instead of 'this.getNewRequest(req)'.
    */
    getNewRequest(req: HttpRequest<any>): HttpRequest<any> {
        if (req.url.indexOf('getData') > 0) {
            return new HttpRequest('GET', 'http://private-4002d-testerrorresponses.apiary-mock.com/getData');
        }

        return new HttpRequest('GET', 'http://private-4002d-testerrorresponses.apiary-mock.com/getLookup');
    }

    logoutUser() {
        // Route to the login page (implementation up to you)

        return observableThrowError("");
    }

    addToken(req: HttpRequest<any>, token: string): HttpRequest<any> {
        return req.clone({ setHeaders: { Authorization: 'Bearer ' + token } });
    }

    private shouldIgnore(url: string): boolean {
        if (url.startsWith(`${this.baseUrl.toLowerCase()}/user/`))
            return true;
    }
}
