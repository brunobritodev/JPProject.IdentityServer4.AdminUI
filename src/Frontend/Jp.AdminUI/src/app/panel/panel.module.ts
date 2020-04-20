import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
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
            { path: "home", loadChildren: () => import("./home/home.module").then(m => m.HomeModule) },
            { path: "clients", loadChildren: () => import("./clients/clients.module").then(m => m.ClientsModule) },
            { path: "identity-resource", loadChildren: () => import("./identity-resources/identity-resource.module").then(m => m.IdentityResourceModule) },
            { path: "api-resource", loadChildren: () => import("./api-resources/api-resource.module").then(m => m.ApiResourceModule) },
            { path: "persisted-grants", loadChildren: () => import("./persisted-grants/persisted-grants.module").then(m => m.PersistedGrantsModule) },
            { path: "users", loadChildren: () => import("./users/user.module").then(m => m.UserModule) },
            { path: "roles", loadChildren: () => import("./roles/roles.module").then(m => m.RoleModule) },
            { path: "emails", loadChildren: () => import("./emails/emails.module").then(m => m.EmailModule) },
            { path: "settings", loadChildren: () => import("./settings/settings.module").then(m => m.SettingsModule) },
            { path: "events", loadChildren: () => import("./events/events.module").then(m => m.EventsModule) }
        ]
    },
];

@NgModule({
    imports: [
        RouterModule.forChild(routes),
    ],
    declarations: [
    ],
    providers: [
        AuthGuardAuthenticadeOnly,
    ],
    exports: [
        RouterModule
    ]
})
export class PanelModule { }
