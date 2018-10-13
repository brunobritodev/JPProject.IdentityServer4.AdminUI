import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SharedModule } from "@shared/shared.module";
import { DndModule } from "ng2-dnd";
import { NgxSelectModule } from 'ngx-select-ex';
import { TagInputModule } from 'ngx-chips';
import { IdentityResourceListComponent } from "./list/identity-resources-list.component";
import { IdentityResourceEditComponent } from "./edit/identity-resource-edit.component";
import { IdentityResourceAddComponent } from "./add/identity-resource-add.component";

const routes: Routes = [
    { path: "", redirectTo: "list", pathMatch: "full" },
    { path: "list", component: IdentityResourceListComponent },
    { path: "edit/:name", component: IdentityResourceEditComponent },
    { path: "add", component: IdentityResourceAddComponent },
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
        IdentityResourceListComponent,
        IdentityResourceEditComponent,
        IdentityResourceAddComponent
    ],
    exports: [
        RouterModule
    ]
})
export class IdentityResourceModule { }
