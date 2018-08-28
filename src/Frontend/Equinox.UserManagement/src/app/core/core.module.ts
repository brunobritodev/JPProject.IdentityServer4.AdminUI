import { NgModule, Optional, SkipSelf, ModuleWithProviders } from "@angular/core";

import { SettingsService } from "./settings/settings.service";
import { throwIfAlreadyLoaded } from "./module-import-guard";

@NgModule({
})
export class CoreModule {
    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
        throwIfAlreadyLoaded(parentModule, "CoreModule");
    }

    static forRoot(): ModuleWithProviders {
        return {
            ngModule: CoreModule,
            providers: [
                SettingsService,
            ]
        };
    }
}
