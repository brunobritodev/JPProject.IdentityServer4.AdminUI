import { NgModule } from "@angular/core";
import { ClientListComponent } from "./list/clients-list.component";
import { Routes, RouterModule } from "@angular/router";
import { SharedModule } from "../../shared/shared.module";
import { DndModule } from "ng2-dnd";

const routes: Routes = [
    { path: "", redirectTo: "list", pathMatch: "full" },
    { path: "list", component: ClientListComponent },
];

@NgModule({
    imports: [
        SharedModule,
        RouterModule.forChild(routes),
        DndModule.forRoot(),
    ],
    declarations: [
        ClientListComponent
    ],
    exports: [
        RouterModule
    ]
})
export class ClientsModule { }
