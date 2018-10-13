import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SharedModule } from "@shared/shared.module";
import { DndModule } from "ng2-dnd";
import { NgxSelectModule } from 'ngx-select-ex';
import { TagInputModule } from 'ngx-chips';
import { ApiResourceListComponent } from "./list/api-resources-list.component";
import { ApiResourceEditComponent } from "./edit/api-resource-edit.component";
import { ApiResourceAddComponent } from "./add/api-resource-add.component";
import { ApiResourceSecretsComponent } from "./secrets/api-secrets.component";
import { ApiResourceScopesComponent } from "./scope/api-scopes.component";

const routes: Routes = [
    { path: "", redirectTo: "list", pathMatch: "full" },
    { path: "list", component: ApiResourceListComponent },
    { path: "edit/:name", component: ApiResourceEditComponent },
    { path: "add", component: ApiResourceAddComponent },
    { path: "secrets/:resource", component: ApiResourceSecretsComponent },
    { path: "scopes/:resource", component: ApiResourceScopesComponent },
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes),
        DndModule.forRoot(),
        NgxSelectModule,
        TagInputModule
    ],
    declarations: [
        ApiResourceListComponent,
        ApiResourceEditComponent,
        ApiResourceAddComponent,
        ApiResourceSecretsComponent,
        ApiResourceScopesComponent
    ],
    exports: [
        RouterModule
    ]
})
export class ApiResourceModule { }
