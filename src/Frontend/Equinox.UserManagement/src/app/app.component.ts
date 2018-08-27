import { Component, OnInit } from "@angular/core";
import { Router, NavigationEnd } from "@angular/router";
import { OAuthService, JwksValidationHandler } from "angular-oauth2-oidc";
import { authConfig } from "./core/auth/auth.config";

@Component({
  // tslint:disable-next-line
  selector: 'body',
  template: "<router-outlet></router-outlet>"
})
export class AppComponent implements OnInit {
  constructor(private router: Router,
    private oauthService: OAuthService) {
    this.configureWithNewConfigApi();
  }

  private async configureWithNewConfigApi() {
    this.oauthService.configure(authConfig);
    this.oauthService.setStorage(localStorage);
    this.oauthService.tokenValidationHandler = new JwksValidationHandler();
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