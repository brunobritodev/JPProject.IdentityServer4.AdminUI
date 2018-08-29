import { Injectable, Inject } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
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

        if (url.startsWith(this.baseUrl)) {
            
            const token = this.authStorage.getAccessToken();
            const header = 'Bearer ' + token;

            const headers = req.headers
                                .set('Authorization', header);

            req = req.clone({ headers });
        }

        return next.handle(req).pipe(
            map(event => event),
            catchError(err => this.handleError(err))
        );
    }






    handleError(error: HttpErrorResponse) {

        console.error('error intercepted', error);

        if (error.status === 401 || error.status === 403) {
            this.router.navigate(['/home', {needsLogin: true}]);
            return of(null);
        }
        return of(error); 
    }

}