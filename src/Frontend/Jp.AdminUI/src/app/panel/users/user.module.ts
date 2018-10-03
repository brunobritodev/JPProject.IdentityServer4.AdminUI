import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SharedModule } from "../../shared/shared.module";
import { DndModule } from "ng2-dnd";
import { NgxSelectModule } from 'ngx-select-ex';
import { TagInputModule } from 'ngx-chips';
import { UserListComponent } from "./list/user-list.component";
import { UserEditComponent } from "./edit/user-edit.component";
import { UserAddComponent } from "./add/user-add.component";
import { UserClaimsComponent } from "./claims/user-claims.component";

const routes: Routes = [
    { path: "", redirectTo: "list", pathMatch: "full" },
    { path: "list", component: UserListComponent },
    { path: "edit/:username", component: UserEditComponent },
    { path: "add", component: UserAddComponent },
    { path: "claims/:username", component: UserClaimsComponent },
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
        UserListComponent,
        UserEditComponent,
        UserAddComponent,
        UserClaimsComponent
    ],
    exports: [
        RouterModule
    ]
})
export class UserModule { }
