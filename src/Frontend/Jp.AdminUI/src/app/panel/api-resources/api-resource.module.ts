import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SharedModule } from "../../shared/shared.module";
import { DndModule } from "ng2-dnd";
import { NgxSelectModule } from 'ngx-select-ex';
import { TagInputModule } from 'ngx-chips';
import { ApiResourceListComponent } from "./list/api-resources-list.component";

const routes: Routes = [
    { path: "", redirectTo: "list", pathMatch: "full" },
    { path: "list", component: ApiResourceListComponent },
    // { path: "edit/:name", component: IdentityResourceEditComponent },
    // { path: "add", component: IdentityResourceAddComponent },
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
    ],
    exports: [
        RouterModule
    ]
})
export class ApiResourceModule { }
