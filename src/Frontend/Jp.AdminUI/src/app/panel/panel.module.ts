import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardAuthenticadeOnly } from '@core/auth/auth-guard-authenticated-only.service';
import { ToasterService } from 'angular2-toaster';

import { SharedModule } from '../shared/shared.module';

const routes: Routes = [
    {
        path: "",
        canActivate: [
            AuthGuardAuthenticadeOnly
        ],
        children: [
            { path: "", redirectTo: "home", pathMatch: "full" },
            { path: "home", loadChildren: "app/panel/home/home.module#HomeModule" },
            { path: "clients", loadChildren: "app/panel/clients/clients.module#ClientsModule" },
            { path: "identity-resource", loadChildren: "app/panel/identity-resources/identity-resource.module#IdentityResourceModule" },
            { path: "api-resource", loadChildren: "app/panel/api-resources/api-resource.module#ApiResourceModule" },
            { path: "persisted-grants", loadChildren: "app/panel/persisted-grants/persisted-grants.module#PersistedGrantsModule" },
            { path: "users", loadChildren: "app/panel/users/user.module#UserModule" },
            { path: "roles", loadChildren: "app/panel/roles/roles.module#RoleModule" },
            { path: "emails", loadChildren: "app/panel/emails/emails.module#EmailModule" },
            { path: "settings", loadChildren: "app/panel/settings/settings.module#SettingsModule" },
            { path: "events", loadChildren: "app/panel/events/events.module#EventsModule" }
        ]
    },
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes),
    ],
    declarations: [
    ],
    providers: [
        AuthGuardAuthenticadeOnly,
        ToasterService
    ],
    exports: [
        RouterModule
    ]
})
export class PanelModule { }
