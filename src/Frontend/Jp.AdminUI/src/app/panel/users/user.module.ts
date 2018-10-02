import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SharedModule } from "../../shared/shared.module";
import { DndModule } from "ng2-dnd";
import { NgxSelectModule } from 'ngx-select-ex';
import { TagInputModule } from 'ngx-chips';
import { UserListComponent } from "./list/user-list.component";
import { UserEditComponent } from "./edit/user-edit.component";

const routes: Routes = [
    { path: "", redirectTo: "list", pathMatch: "full" },
    { path: "list", component: UserListComponent },
    { path: "edit/:username", component: UserEditComponent },
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
        UserEditComponent
    ],
    exports: [
        RouterModule
    ]
})
export class UserModule { }
