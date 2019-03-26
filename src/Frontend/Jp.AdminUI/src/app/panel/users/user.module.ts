import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SharedModule } from "@shared/shared.module";
import { DndModule } from "ng2-dnd";
import { NgxSelectModule } from 'ngx-select-ex';
import { TagInputModule } from 'ngx-chips';
import { UserListComponent } from "./list/user-list.component";
import { UserEditComponent } from "./edit/user-edit.component";
import { UserAddComponent } from "./add/user-add.component";
import { UserClaimsComponent } from "./claims/user-claims.component";
import { UserRolesComponent } from "./roles/user-roles.component";
import { UserLoginsComponent } from "./logins/user-logins.component";
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';



const routes: Routes = [
    { path: "", redirectTo: "list", pathMatch: "full" },
    { path: "list", component: UserListComponent },
    { path: "edit/:username", component: UserEditComponent },
    { path: "add", component: UserAddComponent },
    { path: "claims/:username", component: UserClaimsComponent },
    { path: "roles/:username", component: UserRolesComponent },
    { path: "logins/:username", component: UserLoginsComponent },
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes),
        DndModule.forRoot(),
        NgxSelectModule,
        TagInputModule,
        NgbPaginationModule
    ],
    declarations: [
        UserListComponent,
        UserEditComponent,
        UserAddComponent,
        UserClaimsComponent,
        UserRolesComponent,
        UserLoginsComponent
    ],
    exports: [
        RouterModule
    ]
})
export class UserModule { }
