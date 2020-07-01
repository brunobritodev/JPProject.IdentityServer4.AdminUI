import { HttpClientModule } from '@angular/common/http';
import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';
import { environment } from '@env/environment';
import { VersionService } from '@shared/services/version.service';
import {
    AuthConfig,
    JwksValidationHandler,
    OAuthModule,
    OAuthModuleConfig,
    OAuthStorage,
    ValidationHandler,
} from 'angular-oauth2-oidc';

import { authProdConfig } from './auth/auth-config.prod';
import { AuthGuardAuthenticadeOnly } from './auth/auth-guard-authenticated-only.service';
import { AuthGuardOnlyAdmin } from './auth/auth-guard-only-admin.service';
import { authModuleConfig } from './auth/auth-module-config';
import { AuthService } from './auth/auth.service';
import { MenuService } from './menu/menu.service';
import { throwIfAlreadyLoaded } from './module-import-guard';
import { SettingsService } from './settings/settings.service';
import { ThemesService } from './themes/themes.service';
import { TranslatorService } from './translator/translator.service';
import { authConfig } from './auth/auth-config';

export function storageFactory(): OAuthStorage {
    return localStorage;
}


@NgModule({
    imports: [
        HttpClientModule,
        OAuthModule.forRoot(),
    ],
    providers: [
        SettingsService,
        VersionService,
        ThemesService,
        TranslatorService,
        MenuService,
        AuthService,
        AuthGuardOnlyAdmin,
        AuthGuardAuthenticadeOnly,
    ],
    declarations: [
    ],
    exports: [
    ]
})
export class CoreModule {
    static forRoot(): ModuleWithProviders<CoreModule> {
        return {
            ngModule: CoreModule,
            providers: [
                { provide: AuthConfig, useValue: authProdConfig },
                { provide: OAuthModuleConfig, useValue: authModuleConfig },
                { provide: OAuthStorage, useFactory: storageFactory },
            ]
        };
    }

    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
        throwIfAlreadyLoaded(parentModule, "CoreModule");
    }
}
