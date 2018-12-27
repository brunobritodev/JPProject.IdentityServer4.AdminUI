import { ModuleWithProviders, NgModule, Optional, SkipSelf } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SettingsService } from './settings/settings.service';
import { TranslatorService } from './translator/translator.service';



@NgModule({
    imports: [CommonModule],
    // declarations: [TitleComponent],
    // exports: [TitleComponent],
    providers: [
        TranslatorService,
        SettingsService
    ]
})
export class CoreModule {

    constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
        if (parentModule) {
            throw new Error('CoreModule is already loaded. Import it in the AppModule only');
        }
    }

    static forRoot(): ModuleWithProviders {
        return {
            ngModule: CoreModule,
            providers: [
                TranslatorService,
                SettingsService
            ]
        };
    }
}