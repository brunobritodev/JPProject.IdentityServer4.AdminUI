import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SettingsService } from '@core/settings/settings.service';
import { VersionService } from '@shared/services/version.service';

import { menu } from './core/menu/menu';
import { MenuService } from './core/menu/menu.service';
import { TranslatorService } from './core/translator/translator.service';
import { PagesModule } from './pages/pages.module';
import { LayoutComponent } from './shared/layout/layout.component';

export const routes = [

    { path: "", redirectTo: "login", pathMatch: "full" },
    {
        path: "",
        component: LayoutComponent,
        loadChildren: () => import("./panel/panel.module").then(m => m.PanelModule)
    },

    // 404 Not found
    { path: "**", redirectTo: "not-found" }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes),
        PagesModule,
    ],
    declarations: [
    ],
    exports: [
        RouterModule
    ]
})
export class RoutesModule {
    constructor(public menuService: MenuService, tr: TranslatorService, private settings: SettingsService) {

        this.settings.isLightVersion$.subscribe(lightVersion => {

            if (lightVersion)
                menuService.addMenu(menu.filter(f => f.lightVersion == lightVersion));
            else
                menuService.addMenu(menu);
        });
    }
}
