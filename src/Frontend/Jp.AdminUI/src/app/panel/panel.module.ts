import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SharedModule } from "../shared/shared.module";
import { ToasterService } from "angular2-toaster";
import { AuthGuard } from "../core/auth/auth.guard";

const routes: Routes = [
    {
        path: "",
        canActivate: [
            AuthGuard
        ],
        children: [
            { path: "", redirectTo: "home", pathMatch: "full" },
            { path: "home", loadChildren: "app/panel/home/home.module#HomeModule" },
            { path: "clients", loadChildren: "app/panel/clients/clients.module#ClientsModule" },
            { path: "identity-resource", loadChildren: "app/panel/identity-resources/identity-resource.module#IdentityResourceModule" }
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
        AuthGuard,
        ToasterService
    ],
    exports: [
        RouterModule
    ]
})
export class PanelModule { }
