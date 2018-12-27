import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from "@angular/common/http";
import { PerfectScrollbarModule } from "ngx-perfect-scrollbar";
import { PERFECT_SCROLLBAR_CONFIG } from "ngx-perfect-scrollbar";
import { PerfectScrollbarConfigInterface } from "ngx-perfect-scrollbar";
import { environment } from "../environments/environment";

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
    suppressScrollX: true
};

import { AppComponent } from "./app.component";

// Import containers
import { DefaultLayoutComponent } from "./core";

const APP_CONTAINERS = [
    DefaultLayoutComponent
];

/*
  BEGIN Social login
 */
import {
    SocialLoginModule,
    AuthServiceConfig,
    GoogleLoginProvider,
    FacebookLoginProvider,
} from "angular-6-social-login-v2";

// Configs 
export function getAuthServiceConfigs() {
    const config = new AuthServiceConfig(
        [
            {
                id: FacebookLoginProvider.PROVIDER_ID,
                provider: new FacebookLoginProvider(environment.FacebookClientId)
            },
            {
                id: GoogleLoginProvider.PROVIDER_ID,
                provider: new GoogleLoginProvider(environment.GoogleClientId)
            }
        ]);
    return config;
}
/* END SOCIAL LOGIN */

import {
    AppAsideModule,
    AppBreadcrumbModule,
    AppHeaderModule,
    AppFooterModule,
    AppSidebarModule,
} from "@coreui/angular";

// Import routing module
import { AppRoutingModule } from "./app.routing";

// Import 3rd party components
import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { TabsModule } from "ngx-bootstrap/tabs";
import { ChartsModule } from "ng2-charts/ng2-charts";
import { OAuthModule, OAuthStorage } from "angular-oauth2-oidc";
import { CoreModule } from "./core/core.module";
import { AuthInterceptor } from "./core/interceptors/AuthInterceptor";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ToastrModule } from "ngx-toastr";
import { TranslateModule, TranslateLoader } from "@ngx-translate/core";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";
import { TranslatorService } from "./core/translator/translator.service";

export function createTranslateLoader(http: HttpClient) {
    return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}

@NgModule({
    imports: [
        BrowserModule,
        HttpClientModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: (createTranslateLoader),
                deps: [HttpClient]
            }
        }),
        OAuthModule.forRoot({
            resourceServer: {
                allowedUrls: ["https://localhost:5003"],
                sendAccessToken: true
            }
        }),
        BrowserAnimationsModule, 
        ToastrModule.forRoot(),
        AppRoutingModule,
        AppAsideModule,
        AppBreadcrumbModule.forRoot(),
        AppFooterModule,
        AppHeaderModule,
        AppSidebarModule,
        PerfectScrollbarModule,
        BsDropdownModule.forRoot(),
        TabsModule.forRoot(),
        ChartsModule,
        SocialLoginModule
    ],
    declarations: [
        AppComponent,
        ...APP_CONTAINERS
    ],
    providers: [
        { provide: AuthServiceConfig, useFactory: getAuthServiceConfigs },
        { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },

    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
