import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SharedModule } from "@shared/shared.module";
import { DndModule } from "ng2-dnd";
import { NgxSelectModule } from 'ngx-select-ex';
import { TagInputModule } from 'ngx-chips';
import { RolesListComponent } from "./list/roles-list.component";
import { RoleEditComponent } from "./edit/role-edit.component";
import { RoleAddComponent } from "./add/role-add.component";

const routes: Routes = [
    { path: "", redirectTo: "list", pathMatch: "full" },
    { path: "list", component: RolesListComponent },
    { path: "edit/:role", component: RoleEditComponent },
    { path: "add", component: RoleAddComponent }
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
        RolesListComponent,
        RoleEditComponent,
        RoleAddComponent
    ],
    exports: [
        RouterModule
    ]
})
export class RoleModule { }
