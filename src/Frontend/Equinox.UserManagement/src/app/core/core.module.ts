import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';

import { CommonModule } from '@angular/common';
import { SettingsService } from './settings/settings.service';
import { throwIfAlreadyLoaded } from './module-import-guard';



@NgModule({
    imports: [CommonModule],
    // declarations: [TitleComponent],
    // exports: [TitleComponent],
    providers: [SettingsService]
})
export class CoreModule {
    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
        throwIfAlreadyLoaded(parentModule, "CoreModule");
    }

    static forRoot(): ModuleWithProviders {
        return {
            ngModule: CoreModule,
            providers: [
                SettingsService 
            ]
        };
    }
}