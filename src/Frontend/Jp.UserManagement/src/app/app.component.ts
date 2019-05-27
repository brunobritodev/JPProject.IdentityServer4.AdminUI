import { Component, OnInit } from "@angular/core";
import { Router, NavigationEnd } from "@angular/router";
import { TranslatorService } from '@core/translator/translator.service';
import { Observable } from "rxjs";
import { OAuthenticationService } from "./core/auth/auth.service";

@Component({
    // tslint:disable-next-line
    selector: 'body',
    template: "<router-outlet></router-outlet>",
    providers: [TranslatorService]
})
export class AppComponent implements OnInit {

    isAuthenticated: Observable<boolean>;
    isDoneLoading: Observable<boolean>;
    canActivateProtectedRoutes: Observable<boolean>;

    constructor(private router: Router,
        private authService: OAuthenticationService,
        public translator: TranslatorService) {

        this.isAuthenticated = this.authService.isAuthenticated$;
        this.isDoneLoading = this.authService.isDoneLoading$;
        this.canActivateProtectedRoutes = this.authService.canActivateProtectedRoutes$;

        this.authService.runInitialLoginSequence(); 
    }

    

    ngOnInit() {
        this.router.events.subscribe((evt) => {
            if (!(evt instanceof NavigationEnd)) {
                return;
            }
            window.scrollTo(0, 0);
        });
    }
}